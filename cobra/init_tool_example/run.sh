#!/usr/bin/env bash


if [ -f ./basic ]; then
    rm ./basic
fi

# go run main.go

go build -o basic main.go

./basic

