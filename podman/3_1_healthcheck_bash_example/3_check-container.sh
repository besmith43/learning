#!/usr/bin/env bash


podman healthcheck run test-container

if [ $? -eq 0 ]; then
	echo healthy
fi

podman ps

