#!/usr/bin/env xcrun -sdk macosx swift

// don't run this....
// it came from here:
// https://gist.github.com/GeorgeLyon/bbd443dcabef5ca5ae71ae83db6524ef
// but it only kind of works.  Pressing the print button was fine, but the echo button crashed it


// Displays UI in an NSWindow which can interact with the commandline
// Usage: `echo "Bar" | ./swift-ui-commandline-tool.swift`

import Foundation
import SwiftUI

extension CommandLine {
  static let input: String = { AnyIterator { readLine() }.joined() }()
}

struct App: SwiftUI.App {
  var body: some Scene {
    WindowGroup {
      VStack {
        Text("Hello, George!")
        Button("Print \"Foo\"") { print("Foo") }
	// this isn't working
        // Button("Echo Input") { print(CommandLine.input) }
        Button("Done") { exit(0) }
      }
        .padding(100)
    }
      .windowStyle(HiddenTitleBarWindowStyle())
  }
}

DispatchQueue.main.async {
    NSApplication.shared.setActivationPolicy(.regular)
    NSApplication.shared.activate(ignoringOtherApps: true)
}
App.main()
