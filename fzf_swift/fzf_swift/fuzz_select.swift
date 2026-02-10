//
//  fuzz_select.swift
//  fzf_swift
//
//  Created by Smith, Blake on 2/9/26.
//

import Foundation

struct FuzzyMatch {
    let item: String
    let score: Int
    let matchedIndices: [Int]
}

class FuzzSelect {
    private var items: [String]
    private var query: String = ""
    private var selectedIndex: Int = 0
    private var filteredMatches: [FuzzyMatch] = []
    private var originalTermios: termios?

    init(items: [String]) {
        self.items = items
        self.filteredMatches = items.map { FuzzyMatch(item: $0, score: 0, matchedIndices: []) }
    }

    // MARK: - Fuzzy Matching

    private func fuzzyMatch(pattern: String, target: String) -> FuzzyMatch? {
        if pattern.isEmpty {
            return FuzzyMatch(item: target, score: 0, matchedIndices: [])
        }

        let patternLower = pattern.lowercased()
        let targetLower = target.lowercased()

        var patternIndex = patternLower.startIndex
        var matchedIndices: [Int] = []
        var score = 0
        var consecutiveBonus = 0
        var previousMatchIndex: Int? = nil

        for (index, char) in targetLower.enumerated() {
            if patternIndex < patternLower.endIndex && char == patternLower[patternIndex] {
                matchedIndices.append(index)

                // Scoring
                score += 1

                // Bonus for consecutive matches
                if let prev = previousMatchIndex, index == prev + 1 {
                    consecutiveBonus += 2
                    score += consecutiveBonus
                } else {
                    consecutiveBonus = 0
                }

                // Bonus for matching at word boundaries
                if index == 0 || target[target.index(target.startIndex, offsetBy: index - 1)] == " " ||
                   target[target.index(target.startIndex, offsetBy: index - 1)] == "/" ||
                   target[target.index(target.startIndex, offsetBy: index - 1)] == "_" ||
                   target[target.index(target.startIndex, offsetBy: index - 1)] == "-" {
                    score += 5
                }

                // Bonus for matching uppercase in camelCase
                let originalChar = target[target.index(target.startIndex, offsetBy: index)]
                if originalChar.isUppercase {
                    score += 3
                }

                previousMatchIndex = index
                patternIndex = patternLower.index(after: patternIndex)
            }
        }

        // All pattern characters must match
        if patternIndex == patternLower.endIndex {
            return FuzzyMatch(item: target, score: score, matchedIndices: matchedIndices)
        }

        return nil
    }

    private func updateFilteredItems() {
        if query.isEmpty {
            filteredMatches = items.map { FuzzyMatch(item: $0, score: 0, matchedIndices: []) }
        } else {
            filteredMatches = items.compactMap { fuzzyMatch(pattern: query, target: $0) }
                .sorted { $0.score > $1.score }
        }

        // Keep selection in bounds
        if selectedIndex >= filteredMatches.count {
            selectedIndex = max(0, filteredMatches.count - 1)
        }
    }

    // MARK: - Terminal Handling

    private func enableRawMode() {
        var raw = termios()
        tcgetattr(STDIN_FILENO, &raw)
        originalTermios = raw

        raw.c_lflag &= ~UInt(ECHO | ICANON)
        raw.c_cc.16 = 1  // VMIN
        raw.c_cc.17 = 0  // VTIME

        tcsetattr(STDIN_FILENO, TCSAFLUSH, &raw)
    }

    private func disableRawMode() {
        if var original = originalTermios {
            tcsetattr(STDIN_FILENO, TCSAFLUSH, &original)
        }
    }

    private func readKey() -> UInt8? {
        var char: UInt8 = 0
        let result = read(STDIN_FILENO, &char, 1)
        return result == 1 ? char : nil
    }

    private func hideCursor() {
        print("\u{001B}[?25l", terminator: "")
    }

    private func showCursor() {
        print("\u{001B}[?25h", terminator: "")
    }

    private func clearScreen() {
        print("\u{001B}[2J\u{001B}[H", terminator: "")
    }

    private func moveCursor(row: Int, col: Int) {
        print("\u{001B}[\(row);\(col)H", terminator: "")
    }

    private func clearLine() {
        print("\u{001B}[2K", terminator: "")
    }

    // MARK: - Rendering

    private func render() {
        // Get terminal size
        var ws = winsize()
        _ = ioctl(STDOUT_FILENO, TIOCGWINSZ, &ws)
        let termRows = Int(ws.ws_row)
        let maxRows = max(1, termRows > 2 ? termRows - 2 : 10)  // Default to 10 if no TTY

        clearScreen()

        // Render prompt
        print("\u{001B}[36m> \u{001B}[0m\(query)\u{001B}[5m▌\u{001B}[0m")

        // Render status line
        print("\u{001B}[90m  \(filteredMatches.count)/\(items.count)\u{001B}[0m")

        // Render items
        let displayCount = min(filteredMatches.count, maxRows)
        for i in 0..<displayCount {
            let match = filteredMatches[i]
            let isSelected = i == selectedIndex

            // Selection indicator
            if isSelected {
                print("\u{001B}[36m> \u{001B}[0m", terminator: "")
            } else {
                print("  ", terminator: "")
            }

            // Render item with highlighted matches
            var highlighted = ""
            for (index, char) in match.item.enumerated() {
                if match.matchedIndices.contains(index) {
                    highlighted += "\u{001B}[33m\(char)\u{001B}[0m"  // Yellow for matches
                } else if isSelected {
                    highlighted += "\u{001B}[1m\(char)\u{001B}[0m"  // Bold for selected
                } else {
                    highlighted += String(char)
                }
            }

            print(highlighted)
        }

        fflush(stdout)
    }

    // MARK: - Main Loop

    func run() -> String? {
        // Check if stdin is a TTY
        guard isatty(STDIN_FILENO) != 0 else {
            print("Error: Not running in an interactive terminal")
            return nil
        }

        enableRawMode()
        hideCursor()

        defer {
            showCursor()
            disableRawMode()
            clearScreen()
        }

        render()

        while true {
            guard let char = readKey() else { continue }

            switch char {
            case 27:  // Escape sequence
                // Check for arrow keys
                guard let bracket = readKey(), bracket == 91 else {
                    // Plain escape - cancel
                    return nil
                }

                guard let arrow = readKey() else { continue }

                switch arrow {
                case 65:  // Up arrow
                    if selectedIndex > 0 {
                        selectedIndex -= 1
                    }
                case 66:  // Down arrow
                    if selectedIndex < filteredMatches.count - 1 {
                        selectedIndex += 1
                    }
                default:
                    break
                }

            case 13, 10:  // Enter
                if !filteredMatches.isEmpty {
                    return filteredMatches[selectedIndex].item
                }
                return nil

            case 3:  // Ctrl-C
                return nil

            case 127, 8:  // Backspace
                if !query.isEmpty {
                    query.removeLast()
                    updateFilteredItems()
                }

            case 14:  // Ctrl-N (down)
                if selectedIndex < filteredMatches.count - 1 {
                    selectedIndex += 1
                }

            case 16:  // Ctrl-P (up)
                if selectedIndex > 0 {
                    selectedIndex -= 1
                }

            case 32...126:  // Printable characters
                query.append(Character(UnicodeScalar(char)))
                updateFilteredItems()

            default:
                break
            }

            render()
        }
    }
}

// Convenience function
func fuzzSelect(items: [String], prompt: String? = nil) -> String? {
    if let prompt = prompt {
        print(prompt)
    }
    let selector = FuzzSelect(items: items)
    return selector.run()
}
