// swift-tools-version:5.9
import PackageDescription

let package = Package(
    name: "fzf_swift",
    platforms: [
        .macOS(.v13)
    ],
    products: [
        .library(name: "FuzzSelectLib", targets: ["FuzzSelectLib"])
    ],
    targets: [
        .target(
            name: "FuzzSelectLib",
            path: "Sources/FuzzSelectLib"
        ),
        .testTarget(
            name: "FuzzSelectLibTests",
            dependencies: ["FuzzSelectLib"],
            path: "Tests/FuzzSelectLibTests"
        )
    ]
)
