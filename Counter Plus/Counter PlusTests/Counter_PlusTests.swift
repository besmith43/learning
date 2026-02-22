//
//  Counter_PlusTests.swift
//  Counter PlusTests
//
//  Created by Smith, Blake on 2/9/26.
//

import Testing
@testable import Counter_Plus

struct Counter_PlusTests {

    @Test func extremesTrackAcrossChanges() async throws {
        let counter = Counter(isCloudSyncEnabled: false)

        #expect(counter.count == 0)
        #expect(counter.highestCount == 0)
        #expect(counter.lowestCount == 0)

        counter.increment()
        counter.increment()
        counter.decrement()
        counter.decrement()
        counter.decrement()

        #expect(counter.count == -1)
        #expect(counter.highestCount == 2)
        #expect(counter.lowestCount == -1)
    }

    @Test func resetDoesNotChangeExtremes() async throws {
        let counter = Counter(isCloudSyncEnabled: false)

        counter.increment()
        counter.increment()
        counter.decrement()
        counter.decrement()
        counter.decrement()

        counter.reset()

        #expect(counter.count == 0)
        #expect(counter.highestCount == 2)
        #expect(counter.lowestCount == -1)
    }
}
