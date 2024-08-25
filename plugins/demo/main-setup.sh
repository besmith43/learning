#!/usr/bin/env bash


mkdir app && cd $_

mkdir plugins
cp ../simple-plugin/simple-plugin.so plugins

touch main.go
go mod init example.app

