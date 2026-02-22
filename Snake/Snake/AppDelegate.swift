//
//  AppDelegate.swift
//  Snake
//
//  Created by Blake Smith on 2/10/26.
//

import UIKit
import CloudKit

@MainActor
final class HighScoreStore {
    static let didUpdateNotification = Notification.Name("HighScoreStoreDidUpdate")

    private var database = CKContainer.default().privateCloudDatabase
    private let recordID = CKRecord.ID(recordName: "HighScore")
    private let recordType = "HighScore"
    private let valueKey = "value"
    private let updatedAtKey = "updatedAt"

    private(set) var cachedHighScore = 0

    init() {
        refreshFromCloud()
    }

    func currentHighScore() -> Int {
        cachedHighScore
    }

    @discardableResult
    func saveIfNeeded(score: Int) -> Int {
        guard score > cachedHighScore else {
            return cachedHighScore
        }

        cachedHighScore = score
        postUpdate()
        saveRecord(value: score)
        return cachedHighScore
    }

    func refreshFromCloud() {
        database.fetch(withRecordID: recordID) { [weak self] record, error in
            guard let self else { return }
            Task { @MainActor in
                if let record, let value = record[self.valueKey] as? Int {
                    self.updateCachedScore(value)
                    return
                }

                if let cloudError = error as? CKError, cloudError.code != .unknownItem {
                    return
                }

                self.saveRecord(value: self.cachedHighScore)
            }
        }
    }

    private func saveRecord(value: Int) {
        let record = CKRecord(recordType: recordType, recordID: recordID)
        record[valueKey] = value as NSNumber
        record[updatedAtKey] = Date() as NSDate
        database.save(record) { [weak self] _, _ in
            guard let self else { return }
            Task { @MainActor in
                self.postUpdate()
            }
        }
    }

    private func updateCachedScore(_ value: Int) {
        if value > cachedHighScore {
            cachedHighScore = value
            postUpdate()
        }
    }

    private func postUpdate() {
        NotificationCenter.default.post(name: HighScoreStore.didUpdateNotification, object: self)
    }
}

@main
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?
    var highScoreStore: HighScoreStore?


    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        highScoreStore = HighScoreStore()
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
