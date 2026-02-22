//
//  ContentView.swift
//  ToDoApp
//
//  Created by Blake Smith on 2/17/26.
//

import CloudKit
import SwiftUI

struct CounterExtremes: Equatable {
    var count: Int
    var highestSeen: Int
    var lowestSeen: Int

    init(count: Int) {
        self.count = count
        self.highestSeen = count
        self.lowestSeen = count
    }

    mutating func applyCountChange(_ newCount: Int) {
        count = newCount

        if count > highestSeen {
            highestSeen = count
        }

        if count < lowestSeen {
            lowestSeen = count
        }
    }

    mutating func mergeWithStored(highest: Int?, lowest: Int?) {
        if let highest {
            highestSeen = highest
        } else {
            highestSeen = count
        }

        if let lowest {
            lowestSeen = lowest
        } else {
            lowestSeen = count
        }

        if count > highestSeen {
            highestSeen = count
        }

        if count < lowestSeen {
            lowestSeen = count
        }
    }
}

struct CounterRecord {
    let recordID: CKRecord.ID
    var count: Int
    var highestSeen: Int
    var lowestSeen: Int

    init(recordID: CKRecord.ID, count: Int, highestSeen: Int, lowestSeen: Int) {
        self.recordID = recordID
        self.count = count
        self.highestSeen = highestSeen
        self.lowestSeen = lowestSeen
    }

    init?(record: CKRecord) {
        guard let count = record[CounterRecordKeys.count] as? Int,
              let highest = record[CounterRecordKeys.highestSeen] as? Int,
              let lowest = record[CounterRecordKeys.lowestSeen] as? Int
        else {
            return nil
        }

        self.recordID = record.recordID
        self.count = count
        self.highestSeen = highest
        self.lowestSeen = lowest
    }

    func asRecord(recordType: String) -> CKRecord {
        let record = CKRecord(recordType: recordType, recordID: recordID)
        record[CounterRecordKeys.count] = count as CKRecordValue
        record[CounterRecordKeys.highestSeen] = highestSeen as CKRecordValue
        record[CounterRecordKeys.lowestSeen] = lowestSeen as CKRecordValue
        return record
    }
}

enum CounterRecordKeys {
    static let recordType = "CounterExtremes"
    static let recordName = "CounterExtremesRecord"
    static let count = "count"
    static let highestSeen = "highestSeen"
    static let lowestSeen = "lowestSeen"
}

@MainActor
final class CounterCloudStore {
    private let database: CKDatabase
    private let recordID = CKRecord.ID(recordName: CounterRecordKeys.recordName)
    private var cachedRecord: CKRecord?

    init(container: CKContainer = .default()) {
        self.database = container.privateCloudDatabase
    }

    func fetchRecord() async throws -> CounterRecord? {
        do {
            let record = try await database.record(for: recordID)
            cachedRecord = record
            return CounterRecord(record: record)
        } catch {
            if let ckError = error as? CKError, ckError.code == .unknownItem {
                return nil
            }
            throw error
        }
    }

    func saveRecord(_ record: CounterRecord) async throws {
        let ckRecord: CKRecord

        if let cachedRecord {
            ckRecord = cachedRecord
        } else {
            do {
                let fetched = try await database.record(for: recordID)
                cachedRecord = fetched
                ckRecord = fetched
            } catch {
                if let ckError = error as? CKError, ckError.code == .unknownItem {
                    ckRecord = record.asRecord(recordType: CounterRecordKeys.recordType)
                } else {
                    throw error
                }
            }
        }

        ckRecord[CounterRecordKeys.count] = record.count as CKRecordValue
        ckRecord[CounterRecordKeys.highestSeen] = record.highestSeen as CKRecordValue
        ckRecord[CounterRecordKeys.lowestSeen] = record.lowestSeen as CKRecordValue

        let savedRecord = try await database.save(ckRecord)
        cachedRecord = savedRecord
    }
}

struct ContentView: View {
    @State private var counter = CounterExtremes(count: 0)
    @State private var isLoading = false
    @State private var errorMessage: String?
    @Environment(\.scenePhase) private var scenePhase

    private let cloudStore = CounterCloudStore()

    var body: some View {
        VStack(spacing: 24) {
            Text("Counter")
                .font(.title)
                .fontWeight(.semibold)

            Text("\(counter.count)")
                .font(.system(size: 64, weight: .bold, design: .rounded))
                .monospacedDigit()

            HStack(spacing: 16) {
                Button {
                    counter.applyCountChange(counter.count - 1)
                    saveCounter()
                } label: {
                    Label("Minus", systemImage: "minus.circle.fill")
                }

                Button {
                    counter.applyCountChange(0)
                    saveCounter()
                } label: {
                    Label("Reset", systemImage: "arrow.counterclockwise.circle.fill")
                }

                Button {
                    counter.applyCountChange(counter.count + 1)
                    saveCounter()
                } label: {
                    Label("Plus", systemImage: "plus.circle.fill")
                }
            }
            .labelStyle(.iconOnly)
            .font(.system(size: 32))
            .foregroundStyle(.tint)

            VStack(spacing: 6) {
                Text("Highest: \(counter.highestSeen)")
                Text("Lowest: \(counter.lowestSeen)")
            }
            .font(.headline)

            if isLoading {
                ProgressView()
            }

            if let errorMessage {
                Text(errorMessage)
                    .font(.footnote)
                    .foregroundStyle(.red)
                    .multilineTextAlignment(.center)
            }
        }
        .padding()
        .task {
            await loadCounter()
        }
        .onChange(of: scenePhase) { _, newPhase in
            if newPhase == .active {
                Task {
                    await loadCounter()
                }
            }
        }
    }

    private func loadCounter() async {
        isLoading = true
        defer { isLoading = false }

        do {
            if let record = try await cloudStore.fetchRecord() {
                counter.count = record.count
                counter.mergeWithStored(highest: record.highestSeen, lowest: record.lowestSeen)
            } else {
                await saveCounter(isInitial: true)
            }
        } catch {
            errorMessage = "CloudKit load failed: \(error.localizedDescription)"
        }
    }

    private func saveCounter(isInitial: Bool = false) {
        Task {
            await saveCounter(isInitial: isInitial)
        }
    }

    private func saveCounter(isInitial: Bool) async {
        isLoading = true
        defer { isLoading = false }

        let record = CounterRecord(
            recordID: CKRecord.ID(recordName: CounterRecordKeys.recordName),
            count: counter.count,
            highestSeen: counter.highestSeen,
            lowestSeen: counter.lowestSeen
        )

        do {
            try await cloudStore.saveRecord(record)
            if isInitial {
                errorMessage = nil
            }
        } catch {
            errorMessage = "CloudKit save failed: \(error.localizedDescription)"
        }
    }
}

#Preview {
    ContentView()
}
