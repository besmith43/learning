# CDK Go Hello World App

This CDK project deploys a containerized Go hello world web application with custom domain and HTTPS certificate. The container image is automatically built and pushed to AWS ECR during deployment.

## Architecture

- **ECR Repository**: Stores the container image with lifecycle policies
- **ECS Fargate**: Runs the containerized Go application
- **Application Load Balancer**: Provides HTTPS termination and routing
- **Route53**: Custom domain configuration
- **Certificate Manager**: SSL/TLS certificate for HTTPS

## Prerequisites

1. AWS CLI configured
2. CDK CLI installed (`npm install -g aws-cdk`)
3. Go 1.21+
4. Docker running locally
5. Domain `besmithaws.click` hosted zone in Route53

## Deploy

### Option 1: Using the deploy script (recommended)
```bash
./deploy.sh
```

### Option 2: Manual deployment
```bash
# Bootstrap CDK (first time only)
cdk bootstrap

# Deploy the stack
cdk deploy
```

## What happens during deployment

1. **ECR Repository Creation**: Creates a private ECR repository named `cdk-go-app`
2. **Container Build**: Builds the Docker image from the `./app` directory
3. **Image Push**: Pushes the built image to the ECR repository
4. **Infrastructure Deployment**: Deploys ECS Fargate service with load balancer
5. **DNS Configuration**: Sets up custom domain with HTTPS certificate

## Outputs

After deployment, you'll see:
- **ECR Repository URI**: Where your container image is stored
- **Load Balancer URL**: Direct access to the load balancer

The app will be available at:
- https://besmithaws.click
- https://example.besmithaws.click

## ECR Repository Management

The ECR repository is configured with:
- **Image scanning**: Enabled for security vulnerability detection
- **Lifecycle policy**: Keeps only the 10 most recent images
- **Removal policy**: Set to DESTROY for development (change to RETAIN for production)

## Clean up

```bash
cdk destroy
```

Note: The ECR repository and its images will be deleted due to the DESTROY removal policy.
