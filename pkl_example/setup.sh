#!/usr/bin/env bash


if [ ! -f "$(which pkl)" ]; then
    echo "installing pkl"
    brew install pkl
fi


if [ ! -f "$(which pkl-gen-go)" ]; then
    echo "installing pkl-gen-go"
    go get -tool github.com/apple/pkl-go/cmd/pkl-gen-go
fi



