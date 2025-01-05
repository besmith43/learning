#!/usr/bin/env bash


echo "listing every use of unwrap in your codebase"
grep -ir unwrap --include \*.rs

echo "the total count of instances found"
grep -ir unwrap --include \*.rs | wc -l

