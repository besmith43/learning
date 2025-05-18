#!/usr/bin/env bash


./build.sh

if [ $? -ne 0 ]; then
    echo build failed
    exit 1
fi

javac -d target -cp ./target:lib/junit-jupiter-api-5.12.2.jar src/test/*.java

java -jar lib/junit-platform-console-standalone-1.12.2.jar execute -cp ./target --scan-classpath


