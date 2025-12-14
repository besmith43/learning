#!/bin/bash

PROFILE="personal"
REGION=$(aws configure get region --profile $PROFILE 2>/dev/null || echo "us-east-1")

echo "Checking AWS resources for profile: $PROFILE in region: $REGION"
echo "=================================================="

# EC2 Instances
echo "EC2 Instances:"
aws ec2 describe-instances --profile $PROFILE --region $REGION --query 'Reservations[].Instances[?State.Name!=`terminated`].[InstanceId,State.Name,InstanceType]' --output table 2>/dev/null || echo "No access or no instances"

# S3 Buckets
echo -e "\nS3 Buckets:"
aws s3 ls --profile $PROFILE 2>/dev/null || echo "No access or no buckets"

# RDS Instances
echo -e "\nRDS Instances:"
aws rds describe-db-instances --profile $PROFILE --region $REGION --query 'DBInstances[].DBInstanceIdentifier' --output table 2>/dev/null || echo "No access or no instances"

# Lambda Functions
echo -e "\nLambda Functions:"
aws lambda list-functions --profile $PROFILE --region $REGION --query 'Functions[].FunctionName' --output table 2>/dev/null || echo "No access or no functions"

# CloudFormation Stacks
echo -e "\nCloudFormation Stacks:"
aws cloudformation list-stacks --profile $PROFILE --region $REGION --stack-status-filter CREATE_COMPLETE UPDATE_COMPLETE --query 'StackSummaries[].StackName' --output table 2>/dev/null || echo "No access or no stacks"

# ECS Clusters
echo -e "\nECS Clusters:"
aws ecs list-clusters --profile $PROFILE --region $REGION --query 'clusterArns' --output table 2>/dev/null || echo "No access or no clusters"

echo -e "\nResource check complete."
