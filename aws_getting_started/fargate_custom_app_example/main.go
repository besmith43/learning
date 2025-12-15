package main

import (
	"log"
	"os"

	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscertificatemanager"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsec2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecr"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecs"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecspatterns"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsroute53"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
	"github.com/joho/godotenv"
)

type CdkGoAppStackProps struct {
	awscdk.StackProps
}

func NewCdkGoAppStack(scope constructs.Construct, id string, props *CdkGoAppStackProps) awscdk.Stack {
	stack := awscdk.NewStack(scope, &id, &props.StackProps)

	// Create VPC
	vpc := awsec2.NewVpc(stack, jsii.String("AppVpc"), &awsec2.VpcProps{
		MaxAzs: jsii.Number(2),
	})

	// Create ECR repository for the container image
	ecrRepo := awsecr.NewRepository(stack, jsii.String("AppRepository"), &awsecr.RepositoryProps{
		RepositoryName:  jsii.String("cdk-go-app"),
		RemovalPolicy:   awscdk.RemovalPolicy_DESTROY, // Use RETAIN for production
		ImageScanOnPush: jsii.Bool(true),
		LifecycleRules: &[]*awsecr.LifecycleRule{
			{
				MaxImageCount: jsii.Number(10), // Keep only 10 most recent images
			},
		},
	})

	// Create ECS cluster
	cluster := awsecs.NewCluster(stack, jsii.String("AppCluster"), &awsecs.ClusterProps{
		Vpc: vpc,
	})

	// Get hosted zone
	hostedZone := awsroute53.HostedZone_FromLookup(stack, jsii.String("HostedZone"), &awsroute53.HostedZoneProviderProps{
		DomainName: jsii.String("besmithaws.click"),
	})

	// Create certificate
	certificate := awscertificatemanager.NewCertificate(stack, jsii.String("Certificate"), &awscertificatemanager.CertificateProps{
		DomainName: jsii.String("besmithaws.click"),
		SubjectAlternativeNames: &[]*string{
			jsii.String("example.besmithaws.click"),
		},
		Validation: awscertificatemanager.CertificateValidation_FromDns(hostedZone),
	})

	// Build and push container image to ECR
	containerImage := awsecs.ContainerImage_FromAsset(jsii.String("./app"), nil)

	// Create Fargate service with ALB
	fargateService := awsecspatterns.NewApplicationLoadBalancedFargateService(stack, jsii.String("FargateService"), &awsecspatterns.ApplicationLoadBalancedFargateServiceProps{
		Cluster:        cluster,
		MemoryLimitMiB: jsii.Number(512),
		Cpu:            jsii.Number(256),
		TaskImageOptions: &awsecspatterns.ApplicationLoadBalancedTaskImageOptions{
			Image:         containerImage,
			ContainerPort: jsii.Number(8080),
		},
		DomainName:   jsii.String("besmithaws.click"),
		DomainZone:   hostedZone,
		Certificate:  certificate,
		RedirectHTTP: jsii.Bool(true),
	})

	// Output the ECR repository URI
	awscdk.NewCfnOutput(stack, jsii.String("ECRRepositoryURI"), &awscdk.CfnOutputProps{
		Value:       ecrRepo.RepositoryUri(),
		Description: jsii.String("ECR Repository URI for the container image"),
	})

	// Output the load balancer URL
	awscdk.NewCfnOutput(stack, jsii.String("LoadBalancerURL"), &awscdk.CfnOutputProps{
		Value:       fargateService.LoadBalancer().LoadBalancerDnsName(),
		Description: jsii.String("Load Balancer DNS Name"),
	})

	return stack
}

func main() {
	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}

	app := awscdk.NewApp(nil)

	NewCdkGoAppStack(app, "CdkGoAppStack", &CdkGoAppStackProps{
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
