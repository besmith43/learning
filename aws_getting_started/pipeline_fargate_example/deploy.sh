#!/usr/bin/env bash

# 1. Get your account ID
ACCOUNT_ID=$(aws sts get-caller-identity --profile personal --query Account --output text)

# 2. Bootstrap all regions
cdk bootstrap aws://$ACCOUNT_ID/us-east-1 --profile personal || exit 1
cdk bootstrap aws://$ACCOUNT_ID/us-east-2 --profile personal || exit 1
cdk bootstrap aws://$ACCOUNT_ID/us-west-2 --profile personal || exit 1

# 3. Deploy the pipeline stack first
cdk deploy FargatePipelineStack --require-approval never --profile personal || exit 1

# 4. Deploy environment stacks
cdk deploy FargateAlphaStack --region us-east-1 --profile personal || exit 1
cdk deploy FargateBetaStack --region us-east-2 --profile personal || exit 1
cdk deploy FargateProdStack --region us-west-2 --profile personal || exit 1