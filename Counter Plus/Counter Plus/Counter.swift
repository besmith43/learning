//
//  Counter.swift
//  Counter Plus
//
//  Created by Smith, Blake on 2/9/26.
//

import Combine
import CloudKit

class Counter: ObservableObject {
    @Published var count = 0
    @Published var highestCount = 0
    @Published var lowestCount = 0
    @Published var cloudSyncMessage: String?
    private let database = CKContainer.default().privateCloudDatabase
    private let recordID = CKRecord.ID(recordName: "counterExtremes")
    private let isCloudSyncEnabled: Bool

    init(isCloudSyncEnabled: Bool = true) {
        self.isCloudSyncEnabled = isCloudSyncEnabled
        if isCloudSyncEnabled {
            Task {
                await checkAccountStatusAndLoad()
            }
        }
    }

    func increment() {
        count += 1
        updateExtremes()
    }

    func decrement() {
        count -= 1
        updateExtremes()
    }

    func reset() {
        count = 0
    }

    private func updateExtremes() {
        var didChange = false
        if count > highestCount {
            highestCount = count
            didChange = true
        }
        if count < lowestCount {
            lowestCount = count
            didChange = true
        }
        if didChange, isCloudSyncEnabled {
            Task {
                await saveExtremes()
            }
        }
    }

    private func loadExtremes() async {
        guard isCloudSyncEnabled else {
            return
        }
        do {
            let record = try await database.record(for: recordID)
            let highest = record["highest"] as? Int ?? 0
            let lowest = record["lowest"] as? Int ?? 0
            await MainActor.run {
                highestCount = highest
                lowestCount = lowest
                cloudSyncMessage = nil
            }
        } catch {
            if let ckError = error as? CKError, ckError.code == .unknownItem {
                await saveExtremes()
            } else {
                await MainActor.run {
                    cloudSyncMessage = "Cloud sync error: \(error.localizedDescription)"
                }
            }
        }
    }

    private func saveExtremes() async {
        guard isCloudSyncEnabled else {
            return
        }
        let record = CKRecord(recordType: "CounterExtremes", recordID: recordID)
        record["highest"] = highestCount
        record["lowest"] = lowestCount
        do {
            _ = try await database.save(record)
            await MainActor.run {
                cloudSyncMessage = nil
            }
        } catch {
            await MainActor.run {
                cloudSyncMessage = "Cloud sync error: \(error.localizedDescription)"
            }
        }
    }

    private func checkAccountStatusAndLoad() async {
        do {
            let status = try await fetchAccountStatus()
            if status == .available {
                await loadExtremes()
            } else {
                await MainActor.run {
                    cloudSyncMessage = "iCloud unavailable: \(accountStatusDescription(status))"
                }
            }
        } catch {
            await MainActor.run {
                cloudSyncMessage = "iCloud status error: \(error.localizedDescription)"
            }
        }
    }

    private func fetchAccountStatus() async throws -> CKAccountStatus {
        try await withCheckedThrowingContinuation { continuation in
            CKContainer.default().accountStatus { status, error in
                if let error {
                    continuation.resume(throwing: error)
                    return
                }
                continuation.resume(returning: status)
            }
        }
    }

    private func accountStatusDescription(_ status: CKAccountStatus) -> String {
        switch status {
        case .available:
            return "Available"
        case .noAccount:
            return "No iCloud account"
        case .restricted:
            return "Restricted"
        case .couldNotDetermine:
            return "Could not determine"
        case .temporarilyUnavailable:
            return "Temporarily unavailable"
        @unknown default:
            return "Unknown"
        }
    }
}
