#!/usr/bin/env bash

function check_last_command {
    if [ $1 -ne 0 ]; then
        echo "$2" >&2
        exit 1
    fi
}

if [ -f fargate_hello_world_example ]; then
    echo "removing fargate_hello_world_example binary" >&2
    rm cdk
fi

if [ -d cdk.out ]; then
    echo "removing cdk.out directory" >&2
    rm -r cdk.out
fi


echo "🚀 Starting deployment process..."

# Get AWS account ID and region
ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text --profile personal)
REGION=$(aws configure get region --profile personal)

if [ -z "$REGION" ]; then
    REGION="us-east-1"
    echo "⚠️  No region configured, defaulting to us-east-1"
fi

echo "📍 Deploying to account: $ACCOUNT_ID, region: $REGION"

# Set environment variables for CDK
export CDK_DEFAULT_ACCOUNT=$ACCOUNT_ID
export CDK_DEFAULT_REGION=$REGION



echo "running cdk acknowledge" >&2
cdk acknowledge 34892 --profile personal

check_last_command $? "cdk acknowledge 34892 command failed"

echo "running cdk bootstrap" >&2
cdk bootstrap --profile personal

check_last_command $? "cdk bootstrap command failed"


if [[ "$(uname -a)" == *"arm64"* ]]; then
    docker buildx build --platform linux/amd64 -t hello-world-app:latest .
else
    docker build -t hello-world-app:latest .
fi

echo "🔍 Creating ECR repository URI..."
ECR_URI=$(aws ecr create-repository --repository-name hello-world-app --query 'repository.repositoryUri' --output text --profile personal)

if [ -z "$ECR_URI" ]; then
    ECR_URI=$(aws ecr describe-repositories --repository-names hello-world-app --query "repositories[0].repositoryUri" --output text --profile personal)
fi

echo "📦 ECR Repository URI: $ECR_URI"


echo "🏷️  Tagging image for ECR..."
docker tag hello-world-app:latest $ECR_URI:latest

echo "🔐 Logging into ECR..."
aws ecr get-login-password --region $REGION --profile personal | docker login --username AWS --password-stdin $ECR_URI
echo "⬆️  Pushing image to ECR..."
docker push $ECR_URI:latest


# echo "running go build" >&2
# go build

# check_last_command $? "go build command failed"

echo "cdk listing:" >&2
cdk list --profile personal

# cdk synth --profile personal

# check_last_command $? "cdk synth command failed"

echo "running cdk deploy" >&2
# cdk deploy does have --require-approval with options like never, broadening
time cdk deploy --verbose --debug --yes --profile personal

check_last_command $? "cdk deploy command failed"

