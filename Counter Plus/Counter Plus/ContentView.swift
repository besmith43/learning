//
//  ContentView.swift
//  Counter Plus
//
//  Created by Smith, Blake on 2/9/26.
//

import SwiftUI

struct ContentView: View {
    @StateObject private var counter = Counter()

    var body: some View {
        VStack(spacing: 40) {
            Text("\(counter.count)")
                .font(.system(size: 80, weight: .bold, design: .rounded))
                .foregroundStyle(.primary)

            HStack(spacing: 30) {
                Button {
                    counter.decrement()
                } label: {
                    Image(systemName: "minus.circle.fill")
                        .font(.system(size: 50))
                        .foregroundStyle(.red)
                }

                Button {
                    counter.reset()
                } label: {
                    Image(systemName: "arrow.counterclockwise.circle.fill")
                        .font(.system(size: 50))
                        .foregroundStyle(.secondary)
                }

                Button {
                    counter.increment()
                } label: {
                    Image(systemName: "plus.circle.fill")
                        .font(.system(size: 50))
                        .foregroundStyle(.green)
                }
            }

            VStack(spacing: 8) {
                Text("Highest: \(counter.highestCount)")
                Text("Lowest: \(counter.lowestCount)")
                if let message = counter.cloudSyncMessage {
                    Text(message)
                        .font(.system(size: 14, weight: .regular, design: .rounded))
                        .foregroundStyle(.secondary)
                        .multilineTextAlignment(.center)
                }
            }
            .font(.system(size: 18, weight: .semibold, design: .rounded))
            .foregroundStyle(.secondary)
        }
        .padding()
        .frame(maxWidth: .infinity, maxHeight: .infinity)
        .background(.background)
    }
}

#Preview {
    ContentView()
}
