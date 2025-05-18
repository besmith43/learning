#!/usr/bin/env bash


if [ -d target ]; then
	rm -r target
fi

mkdir target

javac -d target src/java/*.java
# javac -d target src/java/Math.java src/java/main.java
# javac -cp lib/junit-platform-console-standalone-1.12.2.jar -d target src/java/*.java src/test/*.java

exit $?
