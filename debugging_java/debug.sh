#!/usr/bin/env bash


if [ ! -d target ]; then
	mkdir target
elif [ -d target ]; then
	rm -r target
	mkdir target
fi

# requires the -g to supply the debug symbols
javac -g -d target src/Math.java src/main.java


echo "Getting Started:"
echo ""
echo "set a breakpoint - stop at myPackage.Math:5"
echo "run the program - run"
echo "continue program execution - cont"
echo "list all local variables - locals"
echo "run the next instruction - step"
echo ""

# run the debugger
jdb -classpath ./target myPackage.Main


