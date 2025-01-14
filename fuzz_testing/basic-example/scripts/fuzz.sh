#!/usr/bin/env bash

trap "rustup default stable" SIGINT


rustup default nightly

cargo fuzz run --target aarch64-apple-darwin fuzz_target_add



