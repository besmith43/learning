#!/usr/bin/env bash


status=$(podman machine info | grep -i machinestate | awk '{ print $2 }')

if [ $status != "Running" ]; then
	echo podman machine is not running
	exit 1
fi

podman build -t go-move-test .

if [ $? -ne 0 ]; then
	echo podman failed to build container
	exit 1
fi

echo
echo

echo running test container

echo
echo

podman run --rm --name move-test go-move-test


