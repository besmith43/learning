//
//  ReadlineBufferTests.swift
//  FuzzSelectLibTests
//
//  Created by Smith, Blake on 2/9/26.
//

import XCTest
@testable import FuzzSelectLib

final class ReadlineBufferTests: XCTestCase {

    // MARK: - Initial State

    func testInitialState() {
        let buffer = ReadlineBuffer()
        XCTAssertEqual(buffer.text, "")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    // MARK: - Character Insertion

    func testInsertCharacterAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("a")
        XCTAssertEqual(buffer.text, "a")
        XCTAssertEqual(buffer.cursorPos, 1)
    }

    func testInsertMultipleCharacters() {
        var buffer = ReadlineBuffer()
        buffer.insert("h")
        buffer.insert("e")
        buffer.insert("l")
        buffer.insert("l")
        buffer.insert("o")
        XCTAssertEqual(buffer.text, "hello")
        XCTAssertEqual(buffer.cursorPos, 5)
    }

    func testInsertCharacterInMiddle() {
        var buffer = ReadlineBuffer()
        buffer.insert("hllo")
        buffer.moveToStart()
        buffer.moveRight()
        buffer.insert("e")
        XCTAssertEqual(buffer.text, "hello")
        XCTAssertEqual(buffer.cursorPos, 2)
    }

    func testInsertString() {
        var buffer = ReadlineBuffer()
        buffer.insert("world")
        XCTAssertEqual(buffer.text, "world")
        XCTAssertEqual(buffer.cursorPos, 5)
    }

    // MARK: - Cursor Movement

    func testMoveLeft() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveLeft()
        XCTAssertEqual(buffer.cursorPos, 4)
        buffer.moveLeft()
        XCTAssertEqual(buffer.cursorPos, 3)
    }

    func testMoveLeftAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        buffer.moveLeft()
        XCTAssertEqual(buffer.cursorPos, 0) // Should stay at 0
    }

    func testMoveRight() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        buffer.moveRight()
        XCTAssertEqual(buffer.cursorPos, 1)
        buffer.moveRight()
        XCTAssertEqual(buffer.cursorPos, 2)
    }

    func testMoveRightAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveRight()
        XCTAssertEqual(buffer.cursorPos, 5) // Should stay at end
    }

    func testMoveToStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        XCTAssertEqual(buffer.cursorPos, 11)
        buffer.moveToStart()
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testMoveToEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.moveToStart()
        buffer.moveToEnd()
        XCTAssertEqual(buffer.cursorPos, 11)
    }

    // MARK: - Word Movement

    func testMoveToPreviousWord() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world test")
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 12) // Before "test"
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 6) // Before "world"
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 0) // Before "hello"
    }

    func testMoveToPreviousWordAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testMoveToNextWord() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world test")
        buffer.moveToStart()
        buffer.moveToNextWord()
        XCTAssertEqual(buffer.cursorPos, 6) // After "hello "
        buffer.moveToNextWord()
        XCTAssertEqual(buffer.cursorPos, 12) // After "world "
        buffer.moveToNextWord()
        XCTAssertEqual(buffer.cursorPos, 16) // End
    }

    func testMoveToNextWordAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToNextWord()
        XCTAssertEqual(buffer.cursorPos, 5) // Should stay at end
    }

    func testWordMovementWithPunctuation() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello-world_test")
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 12) // Before "test"
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 6) // Before "world"
    }

    // MARK: - Backspace (Delete Backward)

    func testDeleteBackward() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        let result = buffer.deleteBackward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "hell")
        XCTAssertEqual(buffer.cursorPos, 4)
    }

    func testDeleteBackwardInMiddle() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveLeft()
        buffer.moveLeft()
        let result = buffer.deleteBackward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "helo")
        XCTAssertEqual(buffer.cursorPos, 2)
    }

    func testDeleteBackwardAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        let result = buffer.deleteBackward()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    // MARK: - Delete Forward

    func testDeleteForward() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        let result = buffer.deleteForward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "ello")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testDeleteForwardInMiddle() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        buffer.moveRight()
        buffer.moveRight()
        let result = buffer.deleteForward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "helo")
        XCTAssertEqual(buffer.cursorPos, 2)
    }

    func testDeleteForwardAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        let result = buffer.deleteForward()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
        XCTAssertEqual(buffer.cursorPos, 5)
    }

    // MARK: - Delete Word Backward (Ctrl-W)

    func testDeleteWordBackward() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        let result = buffer.deleteWordBackward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "hello ")
        XCTAssertEqual(buffer.cursorPos, 6)
    }

    func testDeleteWordBackwardMultiple() {
        var buffer = ReadlineBuffer()
        buffer.insert("one two three")
        _ = buffer.deleteWordBackward()
        XCTAssertEqual(buffer.text, "one two ")
        _ = buffer.deleteWordBackward()
        XCTAssertEqual(buffer.text, "one ")
        _ = buffer.deleteWordBackward()
        XCTAssertEqual(buffer.text, "")
    }

    func testDeleteWordBackwardAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        let result = buffer.deleteWordBackward()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
    }

    // MARK: - Delete Word Forward (Alt-D)

    func testDeleteWordForward() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.moveToStart()
        let result = buffer.deleteWordForward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "world")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testDeleteWordForwardInMiddle() {
        var buffer = ReadlineBuffer()
        buffer.insert("one two three")
        buffer.moveToStart()
        buffer.moveToNextWord() // After "one "
        let result = buffer.deleteWordForward()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "one three")
    }

    func testDeleteWordForwardAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        let result = buffer.deleteWordForward()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
    }

    // MARK: - Kill to End (Ctrl-K)

    func testKillToEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.moveToStart()
        buffer.moveRight()
        buffer.moveRight()
        buffer.moveRight()
        buffer.moveRight()
        buffer.moveRight()
        let result = buffer.killToEnd()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "hello")
        XCTAssertEqual(buffer.cursorPos, 5)
    }

    func testKillToEndAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        let result = buffer.killToEnd()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
    }

    func testKillToEndFromStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        let result = buffer.killToEnd()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    // MARK: - Kill to Start (Ctrl-U)

    func testKillToStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.moveToStart()
        for _ in 0..<6 { buffer.moveRight() }
        let result = buffer.killToStart()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "world")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testKillToStartAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        let result = buffer.killToStart()
        XCTAssertFalse(result)
        XCTAssertEqual(buffer.text, "hello")
    }

    func testKillToStartFromEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        let result = buffer.killToStart()
        XCTAssertTrue(result)
        XCTAssertEqual(buffer.text, "")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    // MARK: - Clear

    func testClear() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.clear()
        XCTAssertEqual(buffer.text, "")
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    // MARK: - Before/After Cursor Accessors

    func testBeforeAndAfterCursor() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello world")
        buffer.moveToStart()
        for _ in 0..<5 { buffer.moveRight() }
        XCTAssertEqual(buffer.beforeCursor, "hello")
        XCTAssertEqual(buffer.afterCursor, " world")
    }

    func testBeforeAndAfterCursorAtStart() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        buffer.moveToStart()
        XCTAssertEqual(buffer.beforeCursor, "")
        XCTAssertEqual(buffer.afterCursor, "hello")
    }

    func testBeforeAndAfterCursorAtEnd() {
        var buffer = ReadlineBuffer()
        buffer.insert("hello")
        XCTAssertEqual(buffer.beforeCursor, "hello")
        XCTAssertEqual(buffer.afterCursor, "")
    }

    // MARK: - Edge Cases

    func testEmptyBuffer() {
        var buffer = ReadlineBuffer()
        XCTAssertFalse(buffer.deleteBackward())
        XCTAssertFalse(buffer.deleteForward())
        XCTAssertFalse(buffer.deleteWordBackward())
        XCTAssertFalse(buffer.deleteWordForward())
        XCTAssertFalse(buffer.killToEnd())
        XCTAssertFalse(buffer.killToStart())
        buffer.moveLeft()
        buffer.moveRight()
        buffer.moveToPreviousWord()
        buffer.moveToNextWord()
        XCTAssertEqual(buffer.cursorPos, 0)
    }

    func testSingleCharacter() {
        var buffer = ReadlineBuffer()
        buffer.insert("a")
        XCTAssertEqual(buffer.cursorPos, 1)
        buffer.moveLeft()
        XCTAssertEqual(buffer.cursorPos, 0)
        buffer.moveRight()
        XCTAssertEqual(buffer.cursorPos, 1)
        _ = buffer.deleteBackward()
        XCTAssertEqual(buffer.text, "")
    }

    func testWordBoundaryWithNumbers() {
        var buffer = ReadlineBuffer()
        buffer.insert("test123 hello456")
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 8) // Before "hello456"
        buffer.moveToPreviousWord()
        XCTAssertEqual(buffer.cursorPos, 0) // Before "test123"
    }
}
