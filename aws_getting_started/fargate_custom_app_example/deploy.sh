#!/bin/bash

# Deploy script for CDK Go App with ECR
set -e

echo "🚀 Starting CDK deployment with ECR container build..."

# Check if AWS CLI is configured
if ! aws sts get-caller-identity > /dev/null 2>&1; then
    echo "❌ AWS CLI not configured. Please run 'aws configure' first."
    exit 1
fi

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker first."
    exit 1
fi

# Bootstrap CDK if needed (only needs to be done once per account/region)
echo "🔧 Bootstrapping CDK (if needed)..."
cdk bootstrap

# Build and deploy
echo "🏗️  Building and deploying stack..."
cdk deploy --require-approval never

echo "✅ Deployment complete!"
echo "📝 Check the CloudFormation outputs for ECR repository URI and Load Balancer URL"