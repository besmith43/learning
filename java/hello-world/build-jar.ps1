#!/usr/bin/env pwsh

javac firstprogram.java

jar cvfm Demo.jar manifest.txt *.class

java -jar Demo.jar
