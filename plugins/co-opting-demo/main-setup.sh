#!/usr/bin/env bash


if [ ! -d app ]; then
    mkdir app && cd app
else
    cd app
fi

if [ ! -d plugins ]; then
    mkdir plugins
fi

if [ -f ../simple-plugin/simple-plugin.so ]; then
    cp ../simple-plugin/simple-plugin.so plugins
fi

if [ ! -f main.go ]; then
    touch main.go
fi

if [ ! -f go.mod ]; then
    go mod init example.app
fi

