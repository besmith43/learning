#!/usr/bin/env bash


ssh plexmini4 << EOF
	if [ ! -d /Users/besmith/.local/go_server_test ]; then
		mkdir -p /Users/besmith/.local/go_server_test
	else
		rm /Users/besmith/.local/go_server_test/*
	fi

	pgrep test_site | xargs kill
EOF

if [ -f test_site ]; then
	rm test_site
fi

go build -o test_site cmd/api/main.go || exit 1


scp test_site scripts/start.sh .env plexmini4:.local/go_server_test


ssh plexmini4 /Users/besmith/.local/go_server_test/start.sh &

