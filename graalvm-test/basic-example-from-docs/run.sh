#!/usr/bin/env bash


if [ -z "$(which native-image)" ]; then
	echo "you don't have native-image in your path"
	exit 1
fi

if [ -f helloworld ]; then
	echo cleaning up previous binary
	rm helloworld
fi

if [ -f HelloWorld.class ]; then
	echo cleaning up previous class file
	rm HelloWorld.class
fi


javac HelloWorld.java

native-image HelloWorld

if [ $? -ne 0 ]; then
	echo native-image compile failed
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

