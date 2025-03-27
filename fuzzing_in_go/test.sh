#!/usr/bin/env bash

function clean {
    if [ -d testdata ]; then
        echo "removing testdata directory"
        rm -r testdata
    fi
}

clean

# go test -v

go test -fuzz=Fuzz -fuzztime 30s

if [ $? -eq 0 ]; then
    clean
fi
