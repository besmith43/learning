#!/bin/bash

go run context.go &

curl localhost:8090/hello
