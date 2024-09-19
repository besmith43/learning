#!/usr/bin/env bash


podman run --replace -d --name test-container --health-cmd /healthcheck --health-on-failure=none --health-retries=1 health-check-actions

