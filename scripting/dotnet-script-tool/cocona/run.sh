#!/usr/bin/env bash


if [ -d ./publish ]; then
	rm -r publish
fi

dotnet script publish ./main.csx

if [ -f ./publish/osx-arm64/main ]; then
	./publish/osx-arm64/main --help


	echo
	echo
	echo running app with no arguments
	echo
	echo
	./publish/osx-arm64/main this is a fun mess
fi


#
# bash autocomplete
#
# $ source <(./myapp --completion bash)
#
#
# zsh autocomplete
#
# % ./myapp --completion zsh > ~/.zsh/functions
#



