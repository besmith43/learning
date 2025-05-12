#!/usr/bin/env bash


mvn clean package

if [ $? -eq 0 ]; then
    java -jar target/2dgame-1.0-SNAPSHOT.jar
fi

# mvn clean compile

# java -cp target/classes main.Main

