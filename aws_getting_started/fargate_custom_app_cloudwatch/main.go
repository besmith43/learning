package main

import (
	"os"

	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsec2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecr"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecs"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecspatterns"
	"github.com/aws/aws-cdk-go/awscdk/v2/awslogs"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
)

type HelloWorldCdkStackProps struct {
	awscdk.StackProps
}

func NewHelloWorldCdkStack(scope constructs.Construct, id string, props *HelloWorldCdkStackProps) awscdk.Stack {
	var sprops awscdk.StackProps
	if props != nil {
		sprops = props.StackProps
	}
	stack := awscdk.NewStack(scope, &id, &sprops)

	// Create VPC
	vpc := awsec2.NewVpc(stack, jsii.String("HelloWorldVpc"), &awsec2.VpcProps{
		MaxAzs: jsii.Number(3),
	})

	// Create ECR repository
	// repository := awsecr.NewRepository(stack, jsii.String("HelloWorldRepo"), &awsecr.RepositoryProps{
	// RepositoryName: jsii.String("hello-world-app"),
	// RemovalPolicy:  awscdk.RemovalPolicy_DESTROY,
	// })

	// Get ECR repository
	repository := awsecr.Repository_FromRepositoryName(
		stack,
		jsii.String("HelloWorldRepo"),
		jsii.String("hello-world-app"),
	)

	// dockerImage := awsecrassets.NewDockerImageAsset(stack, jsii.String("hello-world-app"), &awsecrassets.DockerImageAssetProps{
	// Directory: jsii.String("."),
	// File:      jsii.String("app/Dockerfile"),
	// })

	// ecrdeploy.NewECRDeployment(stack, jsii.String("DeployDockerImage1"), &ECRDeploymentProps{
	// Src:  ecrdeploy.NewDockerImageName(dockerImage.ImageUri),
	// Dest: ecrdeploy.NewDockerImageName(repository.RepositoryUriForTag(jsii.String("latest"))),
	// })

	// Create ECS cluster
	cluster := awsecs.NewCluster(stack, jsii.String("HelloWorldCluster"), &awsecs.ClusterProps{
		Vpc: vpc,
	})

	// Create CloudWatch log group
	logGroup := awslogs.NewLogGroup(stack, jsii.String("HelloWorldLogGroup"), &awslogs.LogGroupProps{
		LogGroupName:  jsii.String("/ecs/hello-world-app"),
		RemovalPolicy: awscdk.RemovalPolicy_DESTROY,
		Retention:     awslogs.RetentionDays_ONE_WEEK,
	})

	// Create task definition
	taskDefinition := awsecs.NewFargateTaskDefinition(stack, jsii.String("HelloWorldTaskDef"), &awsecs.FargateTaskDefinitionProps{
		MemoryLimitMiB: jsii.Number(512),
		Cpu:            jsii.Number(256),
	})

	// Add container to task definition
	container := taskDefinition.AddContainer(jsii.String("HelloWorldContainer"), &awsecs.ContainerDefinitionOptions{
		Image: awsecs.ContainerImage_FromEcrRepository(repository, jsii.String("latest")),
		Logging: awsecs.LogDriver_AwsLogs(&awsecs.AwsLogDriverProps{
			StreamPrefix: jsii.String("hello-world"),
			LogGroup:     logGroup,
		}),
		Environment: &map[string]*string{
			"PORT": jsii.String("8080"),
		},
	})

	// Add port mapping
	container.AddPortMappings(&awsecs.PortMapping{
		ContainerPort: jsii.Number(8080),
		Protocol:      awsecs.Protocol_TCP,
	})

	// Create Fargate service with Application Load Balancer
	fargateService := awsecspatterns.NewApplicationLoadBalancedFargateService(stack, jsii.String("HelloWorldService"), &awsecspatterns.ApplicationLoadBalancedFargateServiceProps{
		Cluster:            cluster,
		TaskDefinition:     taskDefinition,
		PublicLoadBalancer: jsii.Bool(true),
		DesiredCount:       jsii.Number(2),
		ListenerPort:       jsii.Number(80),
	})

	// Add ECR permissions to task role
	repository.GrantPull(taskDefinition.TaskRole())

	// Output the load balancer URL
	awscdk.NewCfnOutput(stack, jsii.String("LoadBalancerURL"), &awscdk.CfnOutputProps{
		Value: fargateService.LoadBalancer().LoadBalancerDnsName(),
	})

	// Output the ECR repository URI
	awscdk.NewCfnOutput(stack, jsii.String("ECRRepositoryURI"), &awscdk.CfnOutputProps{
		Value: repository.RepositoryUri(),
	})

	return stack
}

func main() {
	defer jsii.Close()

	app := awscdk.NewApp(nil)

	NewHelloWorldCdkStack(app, "HelloWorldCdkStack", &HelloWorldCdkStackProps{
		awscdk.StackProps{
			Env: env(),
		},
	})

	app.Synth(nil)
}

func env() *awscdk.Environment {
	return &awscdk.Environment{
		Account: jsii.String(os.Getenv("CDK_DEFAULT_ACCOUNT")),
		Region:  jsii.String(os.Getenv("CDK_DEFAULT_REGION")),
	}
}
