//
//  GameScene.swift
//  Snake
//
//  Created by Blake Smith on 2/10/26.
//

import SpriteKit

struct GridPoint: Hashable {
    let x: Int
    let y: Int
}

enum Direction {
    case up
    case down
    case left
    case right

    var delta: (x: Int, y: Int) {
        switch self {
        case .up: return (0, 1)
        case .down: return (0, -1)
        case .left: return (-1, 0)
        case .right: return (1, 0)
        }
    }

    func isOpposite(to other: Direction) -> Bool {
        switch (self, other) {
        case (.up, .down), (.down, .up), (.left, .right), (.right, .left):
            return true
        default:
            return false
        }
    }
}

final class GameScene: SKScene {
    var highScoreStore: HighScoreStore?

    private let columns = 18
    private let rows = 30
    private let swipeThreshold: CGFloat = 24

    private var cellSize: CGFloat = 0
    private var boardOrigin = CGPoint.zero
    private var lastUpdateTime: TimeInterval = 0
    private var accumulator: TimeInterval = 0
    private var tickDuration: TimeInterval = 0.16

    private var snake: [GridPoint] = []
    private var direction: Direction = .right
    private var pendingDirection: Direction?
    private var food = GridPoint(x: 0, y: 0)
    private var score = 0
    private var highScore = 0
    private var isGameOver = false
    private var swipeStart: CGPoint?

    private let boardNode = SKShapeNode()
    private let snakeLayer = SKNode()
    private let foodNode = SKShapeNode()
    private let scoreLabel = SKLabelNode(fontNamed: "AvenirNext-Bold")
    private let highScoreLabel = SKLabelNode(fontNamed: "AvenirNext-DemiBold")
    private let statusLabel = SKLabelNode(fontNamed: "AvenirNext-Bold")

    override func didMove(to view: SKView) {
        backgroundColor = SKColor(red: 0.06, green: 0.08, blue: 0.1, alpha: 1)
        configureBoardMetrics()
        configureHUD()
        configureBoard()

        highScore = highScoreStore?.currentHighScore() ?? 0
        startNewGame()
    }

    override func didChangeSize(_ oldSize: CGSize) {
        super.didChangeSize(oldSize)
        configureBoardMetrics()
        configureHUD()
        configureBoard()
        renderSnake()
        renderFood()
    }

    override func update(_ currentTime: TimeInterval) {
        if lastUpdateTime == 0 {
            lastUpdateTime = currentTime
            return
        }

        accumulator += currentTime - lastUpdateTime
        lastUpdateTime = currentTime

        guard !isGameOver else { return }

        while accumulator >= tickDuration {
            accumulator -= tickDuration
            advanceGameStep()
        }
    }

    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        guard let touch = touches.first else { return }
        swipeStart = touch.location(in: self)
    }

    override func touchesMoved(_ touches: Set<UITouch>, with event: UIEvent?) {
        guard let start = swipeStart, let touch = touches.first else { return }
        let current = touch.location(in: self)
        if applySwipe(from: start, to: current) {
            swipeStart = current
        }
    }

    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        if isGameOver {
            startNewGame()
            return
        }

        guard let start = swipeStart, let touch = touches.first else { return }
        let end = touch.location(in: self)
        _ = applySwipe(from: start, to: end)
        swipeStart = nil
    }

    override func touchesCancelled(_ touches: Set<UITouch>, with event: UIEvent?) {
        swipeStart = nil
    }

    private func configureBoardMetrics() {
        let boardWidth = size.width * 0.92
        let boardHeight = size.height * 0.78
        let proposedCell = min(boardWidth / CGFloat(columns), boardHeight / CGFloat(rows))
        cellSize = floor(proposedCell)

        let actualWidth = CGFloat(columns) * cellSize
        let actualHeight = CGFloat(rows) * cellSize
        boardOrigin = CGPoint(
            x: (size.width - actualWidth) / 2,
            y: (size.height - actualHeight) / 2 - 16
        )
    }

    private func configureHUD() {
        scoreLabel.fontSize = 24
        scoreLabel.fontColor = .white
        scoreLabel.horizontalAlignmentMode = .left
        scoreLabel.verticalAlignmentMode = .top
        scoreLabel.position = CGPoint(x: boardOrigin.x, y: boardOrigin.y + CGFloat(rows) * cellSize + 46)
        if scoreLabel.parent == nil {
            addChild(scoreLabel)
        }

        highScoreLabel.fontSize = 18
        highScoreLabel.fontColor = SKColor(red: 0.7, green: 0.84, blue: 1, alpha: 1)
        highScoreLabel.horizontalAlignmentMode = .right
        highScoreLabel.verticalAlignmentMode = .top
        highScoreLabel.position = CGPoint(x: boardOrigin.x + CGFloat(columns) * cellSize, y: boardOrigin.y + CGFloat(rows) * cellSize + 44)
        if highScoreLabel.parent == nil {
            addChild(highScoreLabel)
        }

        statusLabel.fontSize = 28
        statusLabel.fontColor = .white
        statusLabel.numberOfLines = 2
        statusLabel.preferredMaxLayoutWidth = CGFloat(columns) * cellSize - 24
        statusLabel.horizontalAlignmentMode = .center
        statusLabel.verticalAlignmentMode = .center
        statusLabel.position = CGPoint(x: size.width / 2, y: size.height / 2)
        statusLabel.zPosition = 20
        if statusLabel.parent == nil {
            addChild(statusLabel)
        }
    }

    private func configureBoard() {
        let rect = CGRect(
            x: boardOrigin.x,
            y: boardOrigin.y,
            width: CGFloat(columns) * cellSize,
            height: CGFloat(rows) * cellSize
        )
        boardNode.path = CGPath(rect: rect, transform: nil)
        boardNode.fillColor = SKColor(red: 0.12, green: 0.16, blue: 0.2, alpha: 1)
        boardNode.strokeColor = SKColor(red: 0.22, green: 0.3, blue: 0.38, alpha: 1)
        boardNode.lineWidth = 2
        boardNode.zPosition = 0
        if boardNode.parent == nil {
            addChild(boardNode)
        }

        if snakeLayer.parent == nil {
            snakeLayer.zPosition = 5
            addChild(snakeLayer)
        }

        if foodNode.parent == nil {
            foodNode.zPosition = 6
            addChild(foodNode)
        }
    }

    private func startNewGame() {
        score = 0
        tickDuration = 0.16
        accumulator = 0
        isGameOver = false
        statusLabel.text = "Swipe to move"

        let centerX = columns / 2
        let centerY = rows / 2
        snake = [
            GridPoint(x: centerX, y: centerY),
            GridPoint(x: centerX - 1, y: centerY),
            GridPoint(x: centerX - 2, y: centerY)
        ]
        direction = .right
        pendingDirection = nil

        spawnFood()
        renderSnake()
        renderFood()
        updateHUD()
    }

    private func advanceGameStep() {
        if let nextDirection = pendingDirection {
            direction = nextDirection
            pendingDirection = nil
        }

        let head = snake[0]
        let delta = direction.delta
        let newHead = GridPoint(x: head.x + delta.x, y: head.y + delta.y)

        if isOutOfBounds(newHead) || snake.contains(newHead) {
            handleGameOver()
            return
        }

        snake.insert(newHead, at: 0)

        if newHead == food {
            score += 1
            tickDuration = max(0.08, tickDuration * 0.98)
            spawnFood()
            renderFood()
        } else {
            snake.removeLast()
        }

        renderSnake()
        updateHUD()
    }

    private func handleGameOver() {
        isGameOver = true
        highScore = highScoreStore?.saveIfNeeded(score: score) ?? max(highScore, score)
        statusLabel.text = "Game Over\nTap to restart"
        updateHUD()
    }

    private func updateHUD() {
        scoreLabel.text = "Score: \(score)"
        highScoreLabel.text = "High Score: \(highScore)"
    }

    private func spawnFood() {
        let body = Set(snake)
        var candidate = GridPoint(x: Int.random(in: 0..<columns), y: Int.random(in: 0..<rows))
        while body.contains(candidate) {
            candidate = GridPoint(x: Int.random(in: 0..<columns), y: Int.random(in: 0..<rows))
        }
        food = candidate
    }

    private func renderSnake() {
        snakeLayer.removeAllChildren()

        for (index, part) in snake.enumerated() {
            let inset: CGFloat = index == 0 ? 1.2 : 1.8
            let node = SKShapeNode(rectOf: CGSize(width: cellSize - inset * 2, height: cellSize - inset * 2), cornerRadius: index == 0 ? 6 : 4)
            node.position = point(for: part)
            node.fillColor = index == 0
                ? SKColor(red: 0.41, green: 0.95, blue: 0.62, alpha: 1)
                : SKColor(red: 0.2, green: 0.8, blue: 0.44, alpha: 1)
            node.strokeColor = .clear
            snakeLayer.addChild(node)
        }
    }

    private func renderFood() {
        let radius = (cellSize - 4) / 2
        foodNode.path = CGPath(ellipseIn: CGRect(x: -radius, y: -radius, width: radius * 2, height: radius * 2), transform: nil)
        foodNode.fillColor = SKColor(red: 1, green: 0.35, blue: 0.34, alpha: 1)
        foodNode.strokeColor = .clear
        foodNode.position = point(for: food)
    }

    private func point(for grid: GridPoint) -> CGPoint {
        CGPoint(
            x: boardOrigin.x + (CGFloat(grid.x) + 0.5) * cellSize,
            y: boardOrigin.y + (CGFloat(grid.y) + 0.5) * cellSize
        )
    }

    private func isOutOfBounds(_ point: GridPoint) -> Bool {
        point.x < 0 || point.x >= columns || point.y < 0 || point.y >= rows
    }

    @discardableResult
    private func applySwipe(from start: CGPoint, to end: CGPoint) -> Bool {
        let dx = end.x - start.x
        let dy = end.y - start.y
        if abs(dx) < swipeThreshold && abs(dy) < swipeThreshold {
            return false
        }

        if abs(dx) > abs(dy) {
            queueDirection(dx > 0 ? .right : .left)
        } else {
            queueDirection(dy > 0 ? .up : .down)
        }
        statusLabel.text = isGameOver ? "Game Over\nTap to restart" : ""
        return true
    }

    private func queueDirection(_ newDirection: Direction) {
        if newDirection.isOpposite(to: direction) {
            return
        }
        pendingDirection = newDirection
    }
}
