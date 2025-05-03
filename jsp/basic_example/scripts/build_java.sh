#!/usr/bin/env bash


echo building java program

if [ ! -d target ]; then
    mkdir target
fi

if [ -d src/java/hello ] && [ -d target ]; then
    if [ -d lib ]; then
	    javac -d target -cp lib src/java/hello/Hello.java
    else
	    javac -d target src/java/hello/Hello.java
    fi
fi

