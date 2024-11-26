#!/usr/bin/env bash


if [ -z "$(which native-image)" ]; then
	echo "you are missing native-image from your path"
	exit 1
fi

gradle shadowJar

java -jar ./app/build/libs/app-all.jar hello

./hello
