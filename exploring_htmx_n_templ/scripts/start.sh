#!/usr/bin/env bash


if [ -f ~/.local/go_server_test/test_site ]; then
	cd ~/.local/go_server_test
	nohup ./test_site &
fi