#!/usr/bin/env bash


if [ ! -d ~/.java_classpath ]; then
    mkdir ~/.java_classpath
fi

curl https://projectlombok.org/downloads/lombok.jar -o ~/.java_classpath/lombok.jar 

