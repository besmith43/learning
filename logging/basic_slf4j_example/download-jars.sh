#!/usr/bin/env bash


if [ -d lib ]; then
    rm -r lib
fi

mkdir lib

cd lib

wget https://repo1.maven.org/maven2/org/slf4j/slf4j-api/2.0.17/slf4j-api-2.0.17.jar
wget https://repo1.maven.org/maven2/org/slf4j/slf4j-simple/2.0.17/slf4j-simple-2.0.17.jar

