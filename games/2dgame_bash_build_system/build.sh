#!/usr/bin/env bash


javac -d target src/main/java/main/*.java src/main/java/**/*.java


java -cp ./target main.Main

