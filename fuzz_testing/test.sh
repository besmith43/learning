#!/usr/bin/env bash

TEST_CP="target:lib/*"

./build.sh

if [ $? -ne 0 ]; then
    echo build failed
    exit 1
fi

javac -d target -cp "$TEST_CP" src/test/*.java

java -jar lib/junit-platform-console-standalone-1.12.2.jar execute --class-path "$TEST_CP" --scan-classpath

