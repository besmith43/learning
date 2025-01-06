#!/usr/bin/env bash


cargo build

cargo build --release

ls -alh ./target/*/stripped_hello | awk '{ print $9"   "$5 }'

