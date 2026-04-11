#!/usr/bin/env bash



echo compiling java code


if [ -d target ]; then
    rm -r target
fi

mkdir target


if [ -f .build-scripts/setup-env.sh ]; then
    source .build-scripts/setup-env.sh
else
    echo "setup env script missing" >&2
    exit 1
fi


if [ ! -d $CATALINA_HOME/lib ]; then
    echo you are missing a catalina home
    exit 1
fi

javac -d target -cp "./lib/*:$CATALINA_HOME/lib/*" src/java/**/*.java

if [ $? -ne 0 ]; then
    echo "java compile failed"
    exit 1
fi


echo building war file

if [ -d dist ]; then
	rm -r dist
	mkdir dist
fi

mkdir -p dist/WEB-INF/classes
mkdir -p dist/WEB-INF/lib
mkdir -p dist/META-INF/


if [ -d lib ]; then
    cp lib/*.jar dist/WEB-INF/lib
fi

cp -r target/* dist/WEB-INF/classes/
cp src/conf/MANIFEST.MF dist/META-INF

cp src/web/*.jsp dist/

cd dist

zip -r0 ../$warfile *

if [ $? -eq 0 ]; then
    cd ..
    rm -r dist
else
    echo failed to create war
    exit 1
fi

mv $warfile $CATALINA_BASE/webapps/


