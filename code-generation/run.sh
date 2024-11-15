#!/usr/bin/env bash


mvn package

java -cp target/code-generation-1.0-SNAPSHOT.jar org.example.App
