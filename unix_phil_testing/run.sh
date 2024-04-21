#!/bin/bash

cat stdin.txt | dotnet run -- --flag true 1> std.txt 2> err.txt

echo
echo
echo "std.txt file contents"
echo
cat std.txt
echo
echo
echo "err.txt file contents"
echo
cat err.txt


dotnet run -- --flag true 1> std.txt 2> err.txt

echo
echo
echo "std.txt file contents"
echo
cat std.txt
echo
echo
echo "err.txt file contents"
echo
cat err.txt


cat stdin.txt | dotnet run -- --flag true


dotnet run -- --flag true
