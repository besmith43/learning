#!/usr/bin/env bash


echo building java program

if [ -d src/java/hello ] && [ -d target ]; then
	javac -d target -cp lib src/java/hello/Hello.java
fi

