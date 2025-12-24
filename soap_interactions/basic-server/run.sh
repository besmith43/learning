#!/usr/bin/env bash


cd server

go run main.go &

sleep 2

cd ../client

go run main.go

pid="$(ps aux | grep "go.*main" | grep -v grep | awk '{ print $2 }')"

kill $pid

