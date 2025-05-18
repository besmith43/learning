#!/usr/bin/env bash


if [ -d lib ]; then
    rm -r lib
fi

mkdir lib

cd lib

wget https://repo1.maven.org/maven2/com/google/code/gson/gson/2.13.1/gson-2.13.1.jar

