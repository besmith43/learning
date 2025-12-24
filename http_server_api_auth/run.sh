#!/usr/bin/env bash


go run server.go &

sleep 2

go run client.go
go run client.go
go run client.go
go run client.go

# kill %1 # this isn't working....

pid="$(ps aux | grep "go.*server" | grep -v grep | awk '{ print $2 }')"

# echo "killing pid - $pid"

kill $pid

