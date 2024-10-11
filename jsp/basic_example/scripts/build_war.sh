#!/usr/bin/env bash


echo building war file

if [ -d dist ]; then
	rm -r dist
	mkdir dist
fi

mkdir -p dist/WEB-INF/classes
mkdir -p dist/WEB-INF/lib
mkdir -p dist/META-INF/


cp web/*.jsp dist/
cp lib/*.jar dist/WEB-INF/lib
cp -r target/* dist/WEB-INF/classes/
cp src/conf/MANIFEST.MF dist/META-INF

cd dist

zip -r0 ../Hello.war *




