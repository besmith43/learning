#!/bin/bash

set -e

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

# Install Go dependencies for CDK
echo "📦 Installing CDK dependencies..."
go mod tidy

# Bootstrap CDK (if not already done)
echo "🔧 Bootstrapping CDK..."
cdk bootstrap --profile personal

# Deploy the stack
echo "🏗️  Deploying CDK stack..."
cdk deploy --require-approval never --profile personal

# Get ECR repository URI from stack outputs
echo "🔍 Getting ECR repository URI..."
ECR_URI=$(aws cloudformation describe-stacks \
    --stack-name HelloWorldCdkStack \
    --query 'Stacks[0].Outputs[?OutputKey==`ECRRepositoryURI`].OutputValue' \
    --output text \
    --profile personal)

echo "📦 ECR Repository URI: $ECR_URI"

# Build and push Docker image
echo "🐳 Building Docker image..."
docker build -t hello-world-app .

echo "🏷️  Tagging image for ECR..."
docker tag hello-world-app:latest $ECR_URI:latest

echo "🔐 Logging into ECR..."
aws ecr get-login-password --region $REGION --profile personal | docker login --username AWS --password-stdin $ECR_URI

echo "⬆️  Pushing image to ECR..."
docker push $ECR_URI:latest

# Update ECS service to use the new image
echo "🔄 Updating ECS service..."
CLUSTER_NAME=$(aws cloudformation describe-stacks \
    --stack-name HelloWorldCdkStack \
    --query 'Stacks[0].Outputs[?contains(OutputKey, `Cluster`)].OutputValue' \
    --output text 2>/dev/null || echo "HelloWorldCdkStack-HelloWorldCluster")

SERVICE_NAME=$(aws ecs list-services --cluster $CLUSTER_NAME --query 'serviceArns[0]' --output text --profile personal | cut -d'/' -f3)

if [ ! -z "$SERVICE_NAME" ]; then
    echo "🔄 Forcing new deployment of service: $SERVICE_NAME"
    aws ecs update-service --cluster $CLUSTER_NAME --service $SERVICE_NAME --force-new-deployment --profile personal > /dev/null
fi

# Get load balancer URL
LB_URL=$(aws cloudformation describe-stacks \
    --stack-name HelloWorldCdkStack \
    --query 'Stacks[0].Outputs[?OutputKey==`LoadBalancerURL`].OutputValue' \
    --output text \
    --profile personal)

echo ""
echo "✅ Deployment completed successfully!"
echo "🌐 Application URL: http://$LB_URL"
echo "📊 CloudWatch Logs: https://$REGION.console.aws.amazon.com/cloudwatch/home?region=$REGION#logsV2:log-groups/log-group/%2Fecs%2Fhello-world-app"
echo ""
echo "🔍 To test the application:"
echo "   curl http://$LB_URL"
echo "   curl http://$LB_URL/health"
