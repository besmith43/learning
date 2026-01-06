# Fargate Multi-Stage Pipeline CDK Project

This CDK project in Go creates a 3-stage pipeline that deploys a Fargate container to different AWS regions with custom domain names.

## Architecture

- **Alpha Stage**: Deploys to `us-east-1` and is accessible at `alpha.besmithaws.click`
- **Beta Stage**: Deploys to `us-east-2` and is accessible at `beta.besmithaws.click`  
- **Production Stage**: Deploys to `us-west-2` and is accessible at `prod.besmithaws.click`

## Components

### Pipeline Stack
- CodeCommit repository for source code
- CodeBuild project for building Docker images
- CodePipeline with 5 stages:
  1. Source (from CodeCommit)
  2. Build (Docker image build and push to ECR)
  3. Deploy Alpha (us-east-1)
  4. Deploy Beta (us-east-2)
  5. Deploy Production (us-west-2)

### Fargate Stack (per environment)
- VPC with public and private subnets
- ECS Fargate cluster
- Application Load Balancer with SSL certificate
- ECR repository for Docker images
- Route53 records for custom domains

## Prerequisites

1. **AWS CLI configured** with appropriate permissions
2. **CDK CLI installed**: `npm install -g aws-cdk`
3. **Go 1.21+** installed
4. **Domain hosted in Route53**: `besmithaws.click` must be a hosted zone in your AWS account

## Deployment Steps

### 1. Initialize the project
```bash
# Install Go dependencies
go mod tidy

# Bootstrap CDK (if not done before)
cdk bootstrap aws://ACCOUNT-ID/us-east-1
cdk bootstrap aws://ACCOUNT-ID/us-east-2  
cdk bootstrap aws://ACCOUNT-ID/us-west-2
```

### 2. Deploy the pipeline
```bash
# Deploy the main pipeline stack
cdk deploy FargatePipelineStack
```

### 3. Deploy individual environment stacks
```bash
# Deploy Alpha environment
cdk deploy FargateAlphaStack --region us-east-1

# Deploy Beta environment  
cdk deploy FargateBetaStack --region us-east-2

# Deploy Production environment
cdk deploy FargateProdStack --region us-west-2
```

### 4. Push code to trigger pipeline
After the pipeline is deployed, push your application code to the CodeCommit repository to trigger the automated deployment.

## Application

The sample application is a simple nginx server serving an HTML page that displays:
- The current environment (Alpha/Beta/Production)
- The AWS region it's deployed in
- A welcome message

## Customization

### Modify the application
- Edit `index.html` for the web content
- Update `Dockerfile` for different base images or configurations
- Modify `buildspec.yml` for custom build steps

### Adjust infrastructure
- Edit `fargate-stack.go` to modify ECS/Fargate configuration
- Update `pipeline.go` to add approval steps or notifications
- Modify domain names in the respective stack files

## Monitoring

Each environment outputs:
- ECR Repository URI
- Service URL (https://stage.besmithaws.click)

You can monitor the deployments through:
- AWS CodePipeline console
- CloudFormation stacks
- ECS service metrics
- Application Load Balancer metrics

## Security Notes

- SSL certificates are automatically provisioned via ACM
- All traffic is redirected from HTTP to HTTPS
- ECR repositories are created per environment for isolation
- IAM roles follow least privilege principles

## Cleanup

To avoid ongoing charges:
```bash
# Delete the stacks
cdk destroy FargateProdStack --region us-west-2
cdk destroy FargateBetaStack --region us-east-2
cdk destroy FargateAlphaStack --region us-east-1
cdk destroy FargatePipelineStack
```

Note: You may need to manually delete ECR repositories if they contain images.