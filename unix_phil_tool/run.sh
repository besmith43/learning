#!/usr/bin/env bash


go build -o basic main.go

if [ $? -ne 0 ]; then
    echo failed build
    exit 1
fi

cat input.txt | ./basic --id 1 | sed 's/Hello/Goodbye/g'

