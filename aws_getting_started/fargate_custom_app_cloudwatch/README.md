# Hello World CDK Go Project

This project creates a complete AWS infrastructure using CDK in Go that builds and deploys a custom Go hello world application through ECR with CloudWatch logging.

## Architecture

- **Go Hello World App**: Simple HTTP server with request logging
- **ECR Repository**: Stores the Docker image
- **ECS Fargate**: Runs the containerized application
- **Application Load Balancer**: Provides public access
- **CloudWatch Logs**: Captures all HTTP requests and application logs
- **VPC**: Secure networking environment

## Prerequisites

- AWS CLI configured with appropriate permissions
- Docker installed and running
- Go 1.21+ installed
- AWS CDK CLI installed (`npm install -g aws-cdk`)

## Quick Start

1. **Deploy everything**:
   ```bash
   ./deploy.sh
   ```

2. **Test the application**:
   ```bash
   # The deploy script will output the URL, or get it manually:
   curl http://$(aws cloudformation describe-stacks --stack-name HelloWorldCdkStack --query 'Stacks[0].Outputs[?OutputKey==`LoadBalancerURL`].OutputValue' --output text)
   ```

## Manual Deployment Steps

If you prefer to run steps manually:

1. **Install dependencies**:
   ```bash
   go mod tidy
   ```

2. **Bootstrap CDK** (first time only):
   ```bash
   cdk bootstrap
   ```

3. **Deploy infrastructure**:
   ```bash
   cdk deploy
   ```

4. **Build and push Docker image**:
   ```bash
   # Get ECR URI from stack outputs
   ECR_URI=$(aws cloudformation describe-stacks --stack-name HelloWorldCdkStack --query 'Stacks[0].Outputs[?OutputKey==`ECRRepositoryURI`].OutputValue' --output text)
   
   # Build and tag
   docker build -t hello-world-app .
   docker tag hello-world-app:latest $ECR_URI:latest
   
   # Login and push
   aws ecr get-login-password --region $(aws configure get region) | docker login --username AWS --password-stdin $ECR_URI
   docker push $ECR_URI:latest
   ```

5. **Update ECS service**:
   ```bash
   aws ecs update-service --cluster HelloWorldCdkStack-HelloWorldCluster --service <service-name> --force-new-deployment
   ```

## Application Features

- **Health Check**: `GET /health` returns JSON health status
- **Hello World**: `GET /` returns greeting with hostname and timestamp
- **Request Logging**: All HTTP requests are logged with timing information
- **CloudWatch Integration**: Logs are automatically sent to CloudWatch

## Monitoring

View logs in CloudWatch:
- Log Group: `/ecs/hello-world-app`
- Console: AWS Console → CloudWatch → Log Groups

## Cleanup

```bash
cdk destroy
```

## Project Structure

```
.
├── main.go              # CDK infrastructure code
├── app/
│   ├── main.go         # Go hello world application
│   └── go.mod          # App dependencies
├── Dockerfile          # Multi-stage Docker build
├── deploy.sh           # Automated deployment script
├── cdk.json           # CDK configuration
└── go.mod             # CDK dependencies
```