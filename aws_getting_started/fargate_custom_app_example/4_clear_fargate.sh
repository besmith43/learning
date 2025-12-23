#!/usr/bin/env bash


echo "deleting ecr repository"

aws ecr delete-repository --repository-name hello-world-app --force --profile personal

echo "running cdk destroy"
cdk destroy --yes --profile personal

if [ -f fargate_hello_world_example ]; then
    rm fargate_hello_world_example 
fi

if [ -d cdk.out ]; then
    rm -r cdk.out
fi

