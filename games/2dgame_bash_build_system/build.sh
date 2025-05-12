#!/usr/bin/env bash


if [ -d target ]; then
    rm -r target
fi

mkdir target

javac -d target src/main/java/main/*.java src/main/java/**/*.java

if [ $? -ne 0 ]; then
    echo "java compile failed"
    exit 1
fi

cp -r src/main/resources/ target/

java -cp ./target main.Main

