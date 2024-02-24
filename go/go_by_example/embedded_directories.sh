#!/bin/bash

mkdir -p folder
echo "hello go" > folder/single_file.txt
echo "123" > folder/file1.hash
echo "456" > folder/file2.hash

go run embedded_directories.go

rm -r folder
