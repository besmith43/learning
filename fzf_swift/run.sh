#!/bin/bash
set -e

cd "$(dirname "$0")"

swiftc -o fzf_bin fzf_swift/main.swift fzf_swift/fuzz_select.swift
./fzf_bin
