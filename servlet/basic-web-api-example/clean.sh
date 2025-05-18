#!/usr/bin/env bash


echo running clean

if [ -d target ]; then
	rm -r target
	mkdir target
fi

if [ -d lib ]; then
	rm -r lib
	mkdir lib
fi

if [ -n "*.log" ]; then
    rm -f *.log
fi

