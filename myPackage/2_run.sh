#!/usr/bin/env bash


if [ ! -d bin ]; then
	mkdir bin
elif [ -d bin ]; then
	rm -r bin
	mkdir bin
fi

# javac -d bin src/Math.java
javac -d bin src/Math.java src/main.java
# javac -d bin src/*.java
# javac -d bin src/main.java

java -cp ./bin myPackage.Main

