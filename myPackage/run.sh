#!/usr/bin/env bash


if [ ! -d bin2 ]; then
	mkdir bin2
elif [ -d bin2 ]; then
	rm -r bin2
	mkdir bin2
fi

# javac -d bin src/Math.java
javac -d bin2 src2/math.java src2/main.java
# javac -d bin src/*.java
# javac -d bin src/main.java

java -cp ./bin2 myPackage.Main

