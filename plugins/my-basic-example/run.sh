#!/usr/bin/env bash

go build --buildmode=plugin -o ./bin/plugin1.so ./plugin1/plugin1.go


go run main.go

