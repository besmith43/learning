//
//  Counter.swift
//  Counter Plus
//
//  Created by Smith, Blake on 2/9/26.
//

import Combine

class Counter: ObservableObject {
    @Published var count = 0

    func increment() {
        count += 1
    }

    func decrement() {
        count -= 1
    }

    func reset() {
        count = 0
    }
}
