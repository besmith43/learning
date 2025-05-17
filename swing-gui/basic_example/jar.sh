#!/usr/bin/env bash


if [ -f hello-fatjar.jar ]; then
    rm hello-fatjar.jar
fi

jbang export fatjar hello.java


if [ $? -eq 0 ]; then
    java -jar hello-fatjar.jar
fi

