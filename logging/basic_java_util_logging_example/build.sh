#!/usr/bin/env bash


if [ -d target ]; then
	rm -r target
fi

mkdir target

# I don't like that I'm having to
# call the jar specifically to get it to work
javac -cp lib/slf4j-api-2.0.17.jar -d target src/java/*.java

exit $?
