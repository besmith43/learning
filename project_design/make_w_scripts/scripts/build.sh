#!/usr/bin/env bash

if [ ! -d bin ]; then
	echo making bin directory
	mkdir bin
fi

echo executing go build
go build -o bin/program1 cmd/program1/main.go


ls -al bin/program1


