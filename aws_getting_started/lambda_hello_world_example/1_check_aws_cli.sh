#!/usr/bin/env bash

if [ -f .env ]; then
    rm .env
fi

touch .env

awsUser="$(aws sts get-caller-identity --profile personal --query "Account" --output text)"

if [ -z "$awsUser" ]; then
    echo "aws user is blank.  please setup your aws cli" >&2
    exit 1
fi

awsRegion="$(aws configure get region --profile personal)"

if [ -z "$awsRegion" ]; then
    echo "aws region is blank.  please setup your aws cli" >&2
    exit 1
fi


echo "CDK_DEFAULT_ACCOUNT=$awsUser" > .env
echo "CDK_DEFAULT_REGION=$awsRegion" >> .env




