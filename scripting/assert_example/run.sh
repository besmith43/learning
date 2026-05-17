#!/usr/bin/env bash


echo "" >&2
echo "" >&2
echo "running with assets enabled" >&2
java -ea DoWork.java

echo "" >&2
echo "" >&2
echo "running with assets disabled" >&2
java DoWork.java

echo "" >&2
echo "" >&2
