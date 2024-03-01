#!/bin/bash


if [ ! -f $(which gum) ]; then
	echo "gum is not installed"
	exit -1
else
	echo "gum is installed"
fi
