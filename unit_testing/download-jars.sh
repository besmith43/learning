#!/usr/bin/env bash


if [ -d lib ]; then
    rm -r lib
fi

mkdir lib

cd lib

wget https://repo1.maven.org/maven2/org/junit/jupiter/junit-jupiter-api/5.12.2/junit-jupiter-api-5.12.2.jar
wget https://repo1.maven.org/maven2/org/junit/platform/junit-platform-console-standalone/1.12.2/junit-platform-console-standalone-1.12.2.jar

