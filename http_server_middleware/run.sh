#!/usr/bin/env bash


go run main.go &

sleep 2

curl http://localhost:3000
echo
curl http://localhost:3000
echo
curl http://localhost:3000
echo
curl http://localhost:3000
echo

kill %1

