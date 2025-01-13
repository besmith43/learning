#!/usr/bin/env bash


cargo run &

server_pid="$(echo $!)"
echo Server PID: $server_pid

sleep 3

curl http://localhost:7878/
curl http://localhost:7878/sleep &
curl http://localhost:7878/
curl http://localhost:7878/
curl http://localhost:7878/


echo stopping server
kill -9 $server_pid

