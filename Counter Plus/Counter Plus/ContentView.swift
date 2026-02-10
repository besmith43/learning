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
        }
        .padding()
        .frame(maxWidth: .infinity, maxHeight: .infinity)
        .background(.background)
    }
}

#Preview {
    ContentView()
}
