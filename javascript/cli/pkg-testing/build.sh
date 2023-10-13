#!/bin/bash

tsc src/*.ts --outDir lib

pkg --compress GZip -t latest-macos-arm64 lib/main.js -o ./dist/main
