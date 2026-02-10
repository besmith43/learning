//
//  main.swift
//  fzf_swift
//
//  Created by Smith, Blake on 2/9/26.
//

import Foundation

let items = [
    "README.md",
    "Package.swift",
    "Sources/main.swift",
    "Sources/FuzzSelect/fuzz_select.swift",
    "Tests/FuzzSelectTests.swift",
    "build/debug/fzf_swift",
    ".gitignore",
    "Documentation/guide.md",
    "Examples/demo.swift"
]

if let selected = fuzzSelect(items: items) {
    print("Selected: \(selected)")
} else {
    print("Cancelled")
}

