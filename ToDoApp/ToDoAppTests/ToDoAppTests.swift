//
//  ToDoAppTests.swift
//  ToDoAppTests
//
//  Created by Blake Smith on 2/17/26.
//

import Testing
@testable import ToDoApp

struct ToDoAppTests {

    @Test func applyCountChangeUpdatesExtremes() async throws {
        var counter = CounterExtremes(count: 0)

        counter.applyCountChange(5)
        #expect(counter.count == 5)
        #expect(counter.highestSeen == 5)
        #expect(counter.lowestSeen == 0)

        counter.applyCountChange(-3)
        #expect(counter.count == -3)
        #expect(counter.highestSeen == 5)
        #expect(counter.lowestSeen == -3)
    }

    @Test func setExtremesFromStoreHonorsCount() async throws {
        var counter = CounterExtremes(count: 7)

        counter.setExtremesFromStore(highest: 5, lowest: -2)
        #expect(counter.highestSeen == 7)
        #expect(counter.lowestSeen == -2)

        counter.setExtremesFromStore(highest: nil, lowest: nil)
        #expect(counter.highestSeen == 7)
        #expect(counter.lowestSeen == 7)
    }

}
