#!/bin/bash

echo no pipe
echo -------------------------------------------
go run main.go
echo -------------------------------------------
echo

echo with pipe
echo -------------------------------------------
cat input.txt | go run main.go
echo -------------------------------------------
echo


test="saddly ran to the store"

echo with redirect
echo -------------------------------------------
go run main.go < input.txt
#go run main.go < echo "$test"
echo -------------------------------------------
echo

