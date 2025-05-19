#!/usr/bin/env bash


./build.sh

if [ $? -ne 0 ]; then
    echo build failed
    exit 1
fi

javadoc -d docs src/java/*.java
