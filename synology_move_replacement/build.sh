#!/bin/bash

# go tool dist list

env GOOS=linux GOARCH=amd64 go build -o move main.go



