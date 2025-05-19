#!/usr/bin/env bash


./build.sh

if [ $? -ne 0 ]; then
    echo build failed
    exit 1
fi

java -cp lib/*:target Main

