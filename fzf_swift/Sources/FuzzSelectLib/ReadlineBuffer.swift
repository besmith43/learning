//
//  ReadlineBuffer.swift
//  FuzzSelectLib
//
//  Created by Smith, Blake on 2/9/26.
//

import Foundation

/// A readline-style text buffer with cursor support
public struct ReadlineBuffer {
    public private(set) var text: String = ""
    public private(set) var cursorPos: Int = 0

    public init() {}

    // MARK: - Cursor Movement

    public mutating func moveLeft() {
        if cursorPos > 0 {
            cursorPos -= 1
        }
    }

    public mutating func moveRight() {
        if cursorPos < text.count {
            cursorPos += 1
        }
    }

    public mutating func moveToStart() {
        cursorPos = 0
    }

    public mutating func moveToEnd() {
        cursorPos = text.count
    }

    public mutating func moveToPreviousWord() {
        cursorPos = findPreviousWordBoundary()
    }

    public mutating func moveToNextWord() {
        cursorPos = findNextWordBoundary()
    }

    // MARK: - Text Insertion

    /// Insert a single character at cursor position
    public mutating func insert(_ char: Character) {
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.insert(char, at: idx)
        cursorPos += 1
    }

    /// Insert a string at cursor position
    public mutating func insert(_ string: String) {
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.insert(contentsOf: string, at: idx)
        cursorPos += string.count
    }

    // MARK: - Text Deletion

    /// Delete character before cursor (backspace)
    @discardableResult
    public mutating func deleteBackward() -> Bool {
        guard cursorPos > 0 else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos - 1)
        text.remove(at: idx)
        cursorPos -= 1
        return true
    }

    /// Delete character at cursor (delete key)
    @discardableResult
    public mutating func deleteForward() -> Bool {
        guard cursorPos < text.count else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.remove(at: idx)
        return true
    }

    /// Delete word backward (Ctrl-W)
    @discardableResult
    public mutating func deleteWordBackward() -> Bool {
        guard cursorPos > 0 else { return false }
        let newPos = findPreviousWordBoundary()
        let start = text.index(text.startIndex, offsetBy: newPos)
        let end = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(start..<end)
        cursorPos = newPos
        return true
    }

    /// Delete word forward (Alt-D)
    @discardableResult
    public mutating func deleteWordForward() -> Bool {
        guard cursorPos < text.count else { return false }
        let endPos = findNextWordBoundary()
        guard endPos > cursorPos else { return false }
        let start = text.index(text.startIndex, offsetBy: cursorPos)
        let end = text.index(text.startIndex, offsetBy: endPos)
        text.removeSubrange(start..<end)
        return true
    }

    /// Kill to end of line (Ctrl-K)
    @discardableResult
    public mutating func killToEnd() -> Bool {
        guard cursorPos < text.count else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(idx...)
        return true
    }

    /// Kill to beginning of line (Ctrl-U)
    @discardableResult
    public mutating func killToStart() -> Bool {
        guard cursorPos > 0 else { return false }
        let idx = text.index(text.startIndex, offsetBy: cursorPos)
        text.removeSubrange(text.startIndex..<idx)
        cursorPos = 0
        return true
    }

    /// Clear all text
    public mutating func clear() {
        text = ""
        cursorPos = 0
    }

    // MARK: - Word Boundary Detection

    public func findPreviousWordBoundary() -> Int {
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

    public func findNextWordBoundary() -> Int {
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

    public var beforeCursor: String {
        String(text.prefix(cursorPos))
    }

    public var afterCursor: String {
        String(text.suffix(text.count - cursorPos))
    }
}
