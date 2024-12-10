#!/usr/bin/env bash


cd script
./compile.sh 2>&1 >/dev/null
cd ../csproj
./compile.sh 2>&1 >/dev/null

cd ../script
echo "Dotnet Script"
./test.sh

cd ../csproj
echo "Dotnet Project"
./test.sh

