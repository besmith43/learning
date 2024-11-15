#!/usr/bin/env bash


if [ -z "$(which native-image)" ]; then
	echo you are missing native-image from your path
	exit 1
fi

if [ ! -f helloworld.jar ]; then
	echo you are missing the helloworld.jar
	echo go into the helloworld directory
	echo and build the project with the following command:
	echo "    mvn package"
	exit 1
fi

native-image -jar helloworld.jar

if [ $? -ne 0 ]; then
	echo native-image failed to compile your jar file
	exit 1
fi


echo
echo
echo file type of helloworld binary

file helloworld

echo
echo
echo running program
echo
echo

./helloworld

