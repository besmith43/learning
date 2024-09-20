#!/usr/bin/env bash

podID=$(podman ps | grep -i "go-hello" | awk '{ print $1 }')

podman kube generate $podID -f k8s-config.yaml

