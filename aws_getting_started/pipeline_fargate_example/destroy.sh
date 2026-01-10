#!/usr/bin/env bash


ACCOUNT_ID=$(aws sts get-caller-identity --profile personal --query Account --output text)

cdk destroy aws://$ACCOUNT_ID/us-east-1 --profile personal || exit 1
cdk destroy aws://$ACCOUNT_ID/us-east-2 --profile personal || exit 1
cdk destroy aws://$ACCOUNT_ID/us-west-2 --profile personal || exit 1
