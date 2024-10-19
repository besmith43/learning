#!/usr/bin/env bash


if [ $(uname) == "Darwin" ]; then
	open http://localhost:8080/making-a-war
else
	echo "this only works on macos"
fi


