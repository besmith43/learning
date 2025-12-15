package main

import (
	"log"
	"os"

	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscertificatemanager"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsec2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecs"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecspatterns"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsroute53"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"

	"github.com/joho/godotenv"
)

type EcsSampleStackProps struct {
	awscdk.StackProps
}

func NewEcsSampleStack(scope constructs.Construct, id string, props *EcsSampleStackProps) awscdk.Stack {
	stack := awscdk.NewStack(scope, &id, &props.StackProps)

	// VPC
	vpc := awsec2.NewVpc(stack, jsii.String("Vpc"), &awsec2.VpcProps{
		MaxAzs: jsii.Number(2),
	})

	// ECS Cluster
	cluster := awsecs.NewCluster(stack, jsii.String("Cluster"), &awsecs.ClusterProps{
		Vpc: vpc,
	})

	// Route53 Hosted Zone (replace with your domain)
	zone := awsroute53.HostedZone_FromLookup(stack, jsii.String("besmithaws.click"), &awsroute53.HostedZoneProviderProps{
		DomainName: jsii.String("besmithaws.click"), // Replace with your domain
	})

	// SSL Certificate
	cert := awscertificatemanager.NewCertificate(stack, jsii.String("Certificate"), &awscertificatemanager.CertificateProps{
		DomainName: jsii.String("example.besmithaws.click"), // Replace with your subdomain
		Validation: awscertificatemanager.CertificateValidation_FromDns(zone),
	})

	// Fargate Service with ALB
	awsecspatterns.NewApplicationLoadBalancedFargateService(stack, jsii.String("Service"), &awsecspatterns.ApplicationLoadBalancedFargateServiceProps{
		Cluster:        cluster,
		MemoryLimitMiB: jsii.Number(512),
		Cpu:            jsii.Number(256),
		TaskImageOptions: &awsecspatterns.ApplicationLoadBalancedTaskImageOptions{
			Image:         awsecs.ContainerImage_FromRegistry(jsii.String("amazon/amazon-ecs-sample"), nil),
			ContainerPort: jsii.Number(80),
		},
		Certificate:  cert,
		DomainName:   jsii.String("example.besmithaws.click"), // Replace with your subdomain
		DomainZone:   zone,
		RedirectHTTP: jsii.Bool(true),
	})

	return stack
}

func main() {
	app := awscdk.NewApp(nil)

	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}

	NewEcsSampleStack(app, "EcsSampleStack", &EcsSampleStackProps{
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
