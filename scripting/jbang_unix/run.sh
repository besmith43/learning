#!/usr/bin/env bash


export CLASS_PATH="$HOME/.icloud/java_classpath"

echo "running DoWork with nothing"

./DoWork

echo
echo

echo "Running DoWork with all the options"

cat input.txt | ./DoWork testing | sed -e 's/Path/PATH/'

echo

