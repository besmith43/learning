#!/usr/bin/env bash


if [ -z "$(which native-image)" ]; then
	echo "you are missing native-image from your path"
	exit 1
fi


gradle shadowJar

native-image -jar ./app/build/libs/app-all.jar hello



