#!/usr/bin/env bash


echo "regular"
mypy main.py

echo "strict mode"
mypy --strict main.py

