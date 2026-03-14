#!/usr/bin/env bash

javac --release 8 ./GUI.java

# Create the JAR with resources and manifest
jar --create --file GUI.jar --manifest manifest.txt GUI.class img/


native-image --jar ./GUI.jar -o GUI-native

