#!/usr/bin/env bash


if [ -z "$(which cargo-make)" ]; then
    cargo binstall -y cargo-make
fi


