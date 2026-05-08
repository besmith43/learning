#!/usr/bin/env bash


JAVA_HOME="jdk/Contents/Home/"
PATH="jdk/Contents/Home/bin:$PATH"

printf "java path: "

which java

javac HelloWorld.java

if [ $? -eq 0 ]; then
    printf "running hello world java program\n"
    java HelloWorld
fi

