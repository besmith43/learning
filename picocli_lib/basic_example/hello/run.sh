#!/usr/bin/env bash


mvn clean package

java -jar ./target/hello.jar --help

