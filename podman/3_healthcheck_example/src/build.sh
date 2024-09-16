#!/usr/bin/env bash


if [ ! -d bin ]; then
	mkdir bin
fi

go build -o bin/main main.go

