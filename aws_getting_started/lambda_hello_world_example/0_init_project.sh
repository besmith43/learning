#!/usr/bin/env bash


if [ -z "$(which cdk)" ] && [ -f "$(which npm)" ]; then
    echo "you are missing cdk.  installing it through npm" >&2
    npm install -g aws-cdk
elif [ -z "$(which cdk)" ] && [ -z "$(which npm)" ]; then
    echo "you are missing npm and therefore cannot install the cdk application" >&2
    exit 1
fi

cdk init app --language go



