#!/bin/bash

echo no pipe and no args
echo -------------------------------------------
go run main.go
echo -------------------------------------------
echo

echo no pipe and args
echo -------------------------------------------
go run main.go -word=opt -numb=7 -fork  a1 b2 c3
echo -------------------------------------------
echo


echo with pipe and no args
echo -------------------------------------------
cat input.txt | go run main.go
echo -------------------------------------------
echo

echo with pipe and args
echo -------------------------------------------
cat input.txt | go run main.go -word=opt -numb=7 -fork  a1 b2 c3
echo -------------------------------------------
echo

echo with redirect and no args
echo -------------------------------------------
go run main.go < input.txt
echo -------------------------------------------
echo

echo with redirect and args
echo -------------------------------------------
go run main.go -word=opt -numb=7 -fork  a1 b2 c3 < input.txt
echo -------------------------------------------
echo

