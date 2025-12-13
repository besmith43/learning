#!/usr/bin/env bash


cdk destroy --yes

if [ -f cdk ]; then
    rm cdk
fi

if [ -d cdk.out ]; then
    rm -r cdk.out
fi

