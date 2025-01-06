#!/usr/bin/env bash

cargo build
if [ $? -ne 0 ]; then
    exit 1
fi

./target/debug/basic-example --help
