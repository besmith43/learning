#!/usr/bin/env bash

if [ -z $(which sqlc) ]; then
	echo "sqlc not installed"
	exit 1
fi

if [ ! -f schema.sql ]; then
	echo "missing a schema.sql file"
	exit 1
fi

if [ ! -f query.sql ]; then
	echo "missing a query.sql file"
	exit 1
fi

sqlc generate


