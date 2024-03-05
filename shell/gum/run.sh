#!/bin/bash


if [ ! -f $(which gum) ]; then
	echo "gum is not installed"
	exit -1
else
	gum style --border=thick "gum is installed"
fi
