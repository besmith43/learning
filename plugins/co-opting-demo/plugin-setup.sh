#!/usr/bin/env bash


if [ ! -d simple-plugin ]; then
    mkdir simple-plugin && cd simple-plugin
else
    cd simple-plugin
fi

if [ ! -f plugin.go ]; then
    touch plugin.go
fi

if [ ! -f go.mod ]; then
    go mod init simple.plugin
fi

