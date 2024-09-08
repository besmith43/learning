#!/usr/bin/env bash

if [ -f main.asm ]; then
	rm main.asm
fi

# -N disables the optimization
go tool compile -S -N main.go > main.asm


if [ -f main.o ]; then
	rm main.o
fi



