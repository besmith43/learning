#!/bin/bash


# dotnet first

echo Starting Dotnet...

BEGIN=$(date +%s)


for i in {1..1000}
do
	curl -X 'GET' 'http://localhost:5273/CalcFib/20000' -H 'accept: application/json'
done

NOW=$(date +%s)
DIFF=$(($NOW - $BEGIN))
MINS=$(($DIFF / 60))
SECS=$(($DIFF % 60))

echo
echo
printf "dotnet took %02d:%02d to run 100 times" $MINS $SECS

echo
echo

# swift second


echo Starting Swift...

BEGIN=$(date +%s)


for i in {1..1000}
do
	curl -X 'GET' 'http://127.0.0.1:8080/calcFib/20000' -H 'accept: application/json'
done

NOW=$(date +%s)
DIFF=$(($NOW - $BEGIN))
MINS=$(($DIFF / 60))
SECS=$(($DIFF % 60))

echo
echo
printf "swift took %02d:%02d to run 100 times" $MINS $SECS

echo
echo

