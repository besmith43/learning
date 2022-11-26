#!/usr/bin/env pwsh

javac --release 8 ./GUI.java

# for more info on this see the following
# https://docs.oracle.com/javase/tutorial/deployment/jar/basicsindex.html
jar --create --file GUI.jar --manifest manifest.txt GUI.class img/

java -jar GUI.jar
