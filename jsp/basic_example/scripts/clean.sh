#!/usr/bin/env bash


echo running clean

if [ -d target ]; then
	rm -r target
	mkdir target
fi

if [ -n "*.log" ]; then
    rm -f *.log
fi

