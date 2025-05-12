#!/usr/bin/env bash


mvn clean package

# java -cp target/classes main.Main

echo "Main-Class: main.Main" > manifest.txt

jar uvf ./target/2dgame-1.0-SNAPSHOT.jar manifest.txt

rm manifest.txt

java -jar ./target/2dgame-1.0-SNAPSHOT.jar


