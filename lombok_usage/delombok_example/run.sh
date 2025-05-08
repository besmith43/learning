#!/usr/bin/env bash


if [ ! -f ~/.java_classpath/lombok.jar ]; then
    echo "you are missing the lombok jar at the checked path of \"$HOME/.java_classpath\""
    exit 1
fi

if [ ! -f Pet.java ]; then
    echo "you are missing the originial Pet.java file"
    exit 1
fi

java -jar ~/.java_classpath/lombok.jar delombok -p Pet.java


