#!/usr/bin/env bash


podman run -d --rm \
	--volume ./logs:/var/logs \
	--health-cmd /healthcheck.sh \
	--health-on-failure=none \
	--health-retries=1 \
	go-hello

