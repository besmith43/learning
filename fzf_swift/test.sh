#!/bin/bash

# Run unit tests for fzf_swift
# Usage: ./test.sh [--verbose]

set -e

cd "$(dirname "$0")"

if [[ "$1" == "--verbose" || "$1" == "-v" ]]; then
    swift test --verbose
else
    swift test
fi
