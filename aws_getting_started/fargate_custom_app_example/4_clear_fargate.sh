#!/usr/bin/env bash


cdk destroy --yes --profile personal

if [ -f fargate_hello_world_example ]; then
    rm fargate_hello_world_example 
fi

if [ -d cdk.out ]; then
    rm -r cdk.out
fi

