#!/usr/bin/env bash

echo "building hello binary"
time zig build-exe ./src/main.zig

time ./main
