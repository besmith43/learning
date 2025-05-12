#!/usr/bin/env bash


if [ -f 2dgame.jar ]; then
    rm 2dgame.jar
fi

if [ -d target ]; then
    rm -r target
fi

mkdir target

javac -d target src/main/java/main/*.java src/main/java/**/*.java

if [ $? -ne 0 ]; then
    echo "java compile failed"
    exit 1
fi

cp -r src/main/resources/ target/

echo "Main-Class: main.Main" > manifest.txt

# jar uvf ./target/2dgame-1.0-SNAPSHOT.jar manifest.txt

cd target
jar cfvm ../2dgame.jar ../manifest.txt *
cd ..
rm manifest.txt

if [ -f 2dgame.jar ]; then
    java -jar 2dgame.jar
fi

# java -cp ./target main.Main

