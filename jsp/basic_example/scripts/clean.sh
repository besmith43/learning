#!/usr/bin/env bash


echo running clean

if [ -d target ]; then
	rm -r target
	mkdir target
fi

