#!/usr/bin/env bash


if [ -z "$(which templ)" ] || [ -z "$(which localias)" ]; then
	echo "you are missing templ or localias" >&2
	exit 1
fi

# compile .templ files into .go files
templ generate


localias set frontend.test "$(cat .env | grep "PORT" | cut -d = -f 2)"
localias start

go run cmd/api/main.go

localias stop
localias rm frontend.test

