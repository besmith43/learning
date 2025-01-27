#!/usr/bin/env bash


docker build -t xdg-test .

docker run --rm xdg-test

