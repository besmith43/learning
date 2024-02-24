#!/bin/bash

go run http_server.go & 
curl localhost:8090/hello
