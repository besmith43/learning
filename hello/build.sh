#!/usr/bin/env bash


if [ ! -f /opt/homebrew/Cellar/zig/0.13.0/lib/zig/libc/darwin/libSystem.tbd ]; then
    ln -s /Applications/Xcode.app//Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX.sdk/usr/lib/libSystem.B.tbd /opt/homebrew/Cellar/zig/0.13.0/lib/zig/libc/darwin/libSystem.tbd
fi


zig build --summary all

tree zig-out

