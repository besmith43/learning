#!/usr/bin/env bash


lambdaFunction="$(aws lambda list-functions --profile personal --query 'Functions[].FunctionName' --output text)"

if [ -z "$lambdaFunction" ]; then
    echo "getting the lambda function failed" >&2
    exit 1
fi

lambdaUrl="$(aws lambda get-function-url-config --profile personal --function-name "$lambdaFunction" --query 'FunctionUrl' --output text)"

if [ -z "$lambdaUrl" ]; then
    echo "getting the lambda url failed" >&2
    exit 1
fi

curl "$lambdaUrl"

