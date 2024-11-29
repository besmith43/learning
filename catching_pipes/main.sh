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

# references
#
# the -t 0 comes from here
# https://stackoverflow.com/questions/2456750/how-to-check-if-stdin-is-from-the-terminal-or-a-pipe-in-a-shell-script
# the short explanation is that test -t 0 is checking for piped stdin
# while test -t 1 would be checking for piped stdout
# and test -t 2 would be checking for piped stderr
#
#
#
#
