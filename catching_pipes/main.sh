#!/usr/bin/env bash

echo outputing all params
echo $*

echo
echo

if [ -t 0 ]; then
	echo runnning interactively
else
	echo outputing piped data
	read pipedText
	echo $pipedText
fi

echo
echo


