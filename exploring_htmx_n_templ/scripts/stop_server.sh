#!/usr/bin/env bash


ssh plexmini4 <<EOF
	pgrep test_site | xargs kill
EOF
