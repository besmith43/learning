//
//  fuzz_select.swift
//  fzf_swift
//
//  Created by Smith, Blake on 2/9/26.
//

import Foundation

// MARK: - Readline Buffer

/// A readline-style text buffer with cursor support
struct ReadlineBuffer {
    private(set) var text: String = ""
    private(set) var cursorPos: Int = 0

    // MARK: - Cursor Movement

    mutating func moveLeft() {
        if cursorPos > 0 {
            cursorPos -= 1
        }
    }

    mutating func moveRight() {
        if cursorPos < text.count {
            cursorPos += 1
        }
    }

    mutating func moveToStart() {
        cursorPos = 0
    }

    mutating func moveToEnd() {
        cursorPos = text.count
    }

    mutating func moveToPreviousWord() {
        cursorPos = findPreviousWordBoundary()
    }

    mutating func moveToNextWord() {
        cursorPos = findNextWordBoundary()
    }

    // MARK: - Text Insertion

    mutating func insert(_ char: Character) {
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.insert(char, at: idx)
        cursorPos += 1
    }

    mutating func insert(_ string: String) {
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.insert(contentsOf: string, at: idx)
        cursorPos += string.count
    }

    // MARK: - Text Deletion

    /// Delete character before cursor (backspace)
    mutating func deleteBackward() -> Bool {
        guard cursorPos > 0 else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos - 1)
        text.remove(at: idx)
        cursorPos -= 1
        return true
    }

    /// Delete character at cursor (delete key)
    mutating func deleteForward() -> Bool {
        guard cursorPos < text.count else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.remove(at: idx)
        return true
    }

    /// Delete word backward (Ctrl-W)
    mutating func deleteWordBackward() -> Bool {
        guard cursorPos > 0 else { return false }
        let newPos = findPreviousWordBoundary()
        let start = text.index(text.startIndex, offsetBy: newPos)
        let end = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(start..<end)
        cursorPos = newPos
        return true
    }

    /// Delete word forward (Alt-D)
    mutating func deleteWordForward() -> Bool {
        guard cursorPos < text.count else { return false }
        let endPos = findNextWordBoundary()
        guard endPos > cursorPos else { return false }
        let start = text.index(text.startIndex, offsetBy: cursorPos)
        let end = text.index(text.startIndex, offsetBy: endPos)
        text.removeSubrange(start..<end)
        return true
    }

    /// Kill to end of line (Ctrl-K)
    mutating func killToEnd() -> Bool {
        guard cursorPos < text.count else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(idx...)
        return true
    }

    /// Kill to beginning of line (Ctrl-U)
    mutating func killToStart() -> Bool {
        guard cursorPos > 0 else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(text.startIndex..<idx)
        cursorPos = 0
        return true
    }

    /// Clear all text
    mutating func clear() {
        text = ""
        cursorPos = 0
    }

    // MARK: - Word Boundary Detection

    func findPreviousWordBoundary() -> Int {
        guard cursorPos > 0 else { return 0 }

        var pos = cursorPos - 1

        // Skip trailing whitespace/punctuation
        while pos > 0 {
            let idx = text.index(text.startIndex, offsetBy: pos)
            let char = text[idx]
            if char.isLetter || char.isNumber {
                break
            }
            pos -= 1
        }

        // Find start of word
        while pos > 0 {
            let idx = text.index(text.startIndex, offsetBy: pos - 1)
            let char = text[idx]
            if !char.isLetter && !char.isNumber {
                break
            }
            pos -= 1
        }

        return pos
    }

    func findNextWordBoundary() -> Int {
        guard cursorPos < text.count else { return text.count }

        var pos = cursorPos

        // Skip current word
        while pos < text.count {
            let idx = text.index(text.startIndex, offsetBy: pos)
            let char = text[idx]
            if !char.isLetter && !char.isNumber {
                break
            }
            pos += 1
        }

        // Skip whitespace/punctuation
        while pos < text.count {
            let idx = text.index(text.startIndex, offsetBy: pos)
            let char = text[idx]
            if char.isLetter || char.isNumber {
                break
            }
            pos += 1
        }

        return pos
    }

    // MARK: - Accessors for rendering

    var beforeCursor: String {
        String(text.prefix(cursorPos))
    }

    var afterCursor: String {
        String(text.suffix(text.count - cursorPos))
    }
}

// MARK: - Fuzzy Match

struct FuzzyMatch {
    let item: String
    let score: Int
    let matchedIndices: [Int]
}

// MARK: - Fuzz Select

class FuzzSelect {
    private var items: [String]
    private var buffer = ReadlineBuffer()
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
        if buffer.text.isEmpty {
            filteredMatches = items.map { FuzzyMatch(item: $0, score: 0, matchedIndices: []) }
        } else {
            filteredMatches = items.compactMap { fuzzyMatch(pattern: buffer.text, target: $0) }
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

        // Render prompt with cursor at correct position
        print("\u{001B}[36m> \u{001B}[0m\(buffer.beforeCursor)\u{001B}[7m\u{001B}[5m \u{001B}[0m\(buffer.afterCursor)", terminator: "")
        // Move cursor back to position (after the space we used as cursor)
        if !buffer.afterCursor.isEmpty {
            print("\u{001B}[\(buffer.afterCursor.count)D", terminator: "")
        }
        print()  // Newline

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
                guard let next = readKey() else {
                    // Plain escape - cancel
                    return nil
                }

                if next == 91 {  // CSI sequence (arrow keys, etc.)
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
                    case 67:  // Right arrow
                        buffer.moveRight()
                    case 68:  // Left arrow
                        buffer.moveLeft()
                    case 72:  // Home
                        buffer.moveToStart()
                    case 70:  // End
                        buffer.moveToEnd()
                    case 51:  // Delete key (sends ESC [ 3 ~)
                        _ = readKey()  // consume the ~
                        if buffer.deleteForward() {
                            updateFilteredItems()
                        }
                    default:
                        break
                    }
                } else if next == 98 {  // Alt-B (backward word)
                    buffer.moveToPreviousWord()
                } else if next == 102 {  // Alt-F (forward word)
                    buffer.moveToNextWord()
                } else if next == 100 {  // Alt-D (delete word forward)
                    if buffer.deleteWordForward() {
                        updateFilteredItems()
                    }
                }

            case 13, 10:  // Enter
                if !filteredMatches.isEmpty {
                    return filteredMatches[selectedIndex].item
                }
                return nil

            case 3:  // Ctrl-C
                return nil

            case 127, 8:  // Backspace
                if buffer.deleteBackward() {
                    updateFilteredItems()
                }

            case 1:  // Ctrl-A (beginning of line)
                buffer.moveToStart()

            case 5:  // Ctrl-E (end of line)
                buffer.moveToEnd()

            case 11:  // Ctrl-K (kill to end of line)
                if buffer.killToEnd() {
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

            case 21:  // Ctrl-U (clear line)
                if buffer.killToStart() {
                    updateFilteredItems()
                }

            case 23:  // Ctrl-W (delete word backward)
                if buffer.deleteWordBackward() {
                    updateFilteredItems()
                }

            case 32...126:  // Printable characters
                buffer.insert(Character(UnicodeScalar(char)))
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
