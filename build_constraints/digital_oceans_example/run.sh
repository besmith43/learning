#!/bin/bash

# this came from digital oceans
# https://www.digitalocean.com/community/tutorials/customizing-go-binaries-with-build-tags

echo building the free version
go build -o free
./free

echo building the pro version
go build -o pro -tags pro
./pro

echo building the enterprise version
go build -o enterprise -tags enterprise,pro
./enterprise

echo cleaning up
rm free pro enterprise

