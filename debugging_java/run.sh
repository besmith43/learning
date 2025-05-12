#!/usr/bin/env bash


if [ ! -d target ]; then
	mkdir target
elif [ -d target ]; then
	rm -r target
	mkdir target
fi

# javac -d target src/Math.java
javac -d target src/Math.java src/main.java
# javac -d target src/*.java
# javac -d target src/main.java

java -cp ./target myPackage.Main

