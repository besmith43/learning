#!/usr/bin/env bash


if [ -d jdk ]; then
    rm -rf jdk
fi

if [ -f HelloWorld.class ]; then
    rm HelloWorld.class
fi

