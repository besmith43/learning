#!/usr/bin/env bash

if [ ! -d ./bin ]; then
    mkdir bin
fi

go build --buildmode=plugin -o ./bin/plugin1.so ./plugin1/plugin1.go
go build --buildmode=plugin -o ./bin/plugin2.so ./plugin2/plugin2.go
go build --buildmode=plugin -o ./bin/plugin3.so ./plugin3/plugin3.go


go run main.go

