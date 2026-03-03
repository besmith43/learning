#!/usr/bin/env bash


export CLASS_PATH="$HOME/.icloud/java_classpath"

echo "running DoWork with no piped input"

./DoWork

echo
echo

echo "running DoWork with all the fixin's"

cat input.txt | ./DoWork testing | sed -e 's/Path/PATH/'

echo

