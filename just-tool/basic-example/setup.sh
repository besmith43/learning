#!/usr/bin/env bash


if [ -z "$(which just)" ]; then
    cargo binstall -y just
fi


