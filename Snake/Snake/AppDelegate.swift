//
//  AppDelegate.swift
//  Snake
//
//  Created by Blake Smith on 2/10/26.
//

import UIKit
import SwiftData

@Model
final class HighScoreRecord {
    var value: Int
    var updatedAt: Date

    init(value: Int = 0, updatedAt: Date = Date()) {
        self.value = value
        self.updatedAt = updatedAt
    }
}

@MainActor
final class HighScoreStore {
    private let container: ModelContainer
    private let context: ModelContext

    init() throws {
        let schema = Schema([HighScoreRecord.self])
        let configuration = ModelConfiguration(schema: schema, cloudKitDatabase: .automatic)
        container = try ModelContainer(for: schema, configurations: [configuration])
        context = ModelContext(container)
    }

    func currentHighScore() -> Int {
        record().value
    }

    @discardableResult
    func saveIfNeeded(score: Int) -> Int {
        let current = record()
        if score > current.value {
            current.value = score
            current.updatedAt = Date()
            try? context.save()
        }
        return current.value
    }

    private func record() -> HighScoreRecord {
        let descriptor = FetchDescriptor<HighScoreRecord>(
            sortBy: [SortDescriptor(\.updatedAt, order: .reverse)]
        )
        if let existing = try? context.fetch(descriptor).first {
            return existing
        }

        let created = HighScoreRecord()
        context.insert(created)
        try? context.save()
        return created
    }
}

@main
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?
    var highScoreStore: HighScoreStore?


    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        do {
            highScoreStore = try HighScoreStore()
        } catch {
            print("Failed to initialize SwiftData high score store: \(error)")
        }
        return true
    }

    func applicationWillResignActive(_ application: UIApplication) {
        // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
        // Use this method to pause ongoing tasks, disable timers, and invalidate graphics rendering callbacks. Games should use this method to pause the game.
    }

    func applicationDidEnterBackground(_ application: UIApplication) {
        // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    }

    func applicationWillEnterForeground(_ application: UIApplication) {
        // Called as part of the transition from the background to the active state; here you can undo many of the changes made on entering the background.
    }

    func applicationDidBecomeActive(_ application: UIApplication) {
        // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    }


}
