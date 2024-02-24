#!/bin/bash

echo "hello" > /tmp/dat
echo "go" >>   /tmp/dat
go run reading_files.go

rm /tmp/dat
