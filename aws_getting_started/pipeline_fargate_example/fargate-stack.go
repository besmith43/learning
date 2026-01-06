package main

import (
	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscertificatemanager"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsec2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecr"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecs"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecspatterns"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsroute53"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsroute53targets"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
)

type FargateStackProps struct {
	awscdk.StackProps
	Stage      string
	DomainName string
}

func NewFargateStack(scope constructs.Construct, id string, props *FargateStackProps) awscdk.Stack {
	var sprops awscdk.StackProps
	if props != nil {
		sprops = props.StackProps
	}
	stack := awscdk.NewStack(scope, &id, &sprops)

	// Create VPC
	vpc := awsec2.NewVpc(stack, jsii.String("FargateVpc"), &awsec2.VpcProps{
		MaxAzs:      jsii.Number(2),
		NatGateways: jsii.Number(1),
	})

	// Create ECR repository
	repository := awsecr.NewRepository(stack, jsii.String("FargateRepo"), &awsecr.RepositoryProps{
		RepositoryName: jsii.String("fargate-app-" + props.Stage),
		RemovalPolicy:  awscdk.RemovalPolicy_DESTROY,
	})

	// Create ECS cluster
	cluster := awsecs.NewCluster(stack, jsii.String("FargateCluster"), &awsecs.ClusterProps{
		Vpc:         vpc,
		ClusterName: jsii.String("fargate-cluster-" + props.Stage),
	})

	// Get hosted zone for besmithaws.click
	hostedZone := awsroute53.HostedZone_FromLookup(stack, jsii.String("HostedZone"), &awsroute53.HostedZoneProviderProps{
		DomainName: jsii.String("besmithaws.click"),
	})

	// Create SSL certificate
	certificate := awscertificatemanager.NewCertificate(stack, jsii.String("Certificate"), &awscertificatemanager.CertificateProps{
		DomainName: jsii.String(props.DomainName),
		Validation: awscertificatemanager.CertificateValidation_FromDns(hostedZone),
	})

	// Create Fargate service with ALB
	fargateService := awsecspatterns.NewApplicationLoadBalancedFargateService(stack, jsii.String("FargateService"), &awsecspatterns.ApplicationLoadBalancedFargateServiceProps{
		Cluster:     cluster,
		ServiceName: jsii.String("fargate-service-" + props.Stage),
		TaskImageOptions: &awsecspatterns.ApplicationLoadBalancedTaskImageOptions{
			Image:         awsecs.ContainerImage_FromEcrRepository(repository, jsii.String("latest")),
			ContainerPort: jsii.Number(80),
			Environment: &map[string]*string{
				"STAGE": jsii.String(props.Stage),
			},
		},
		MemoryLimitMiB:     jsii.Number(512),
		Cpu:                jsii.Number(256),
		DesiredCount:       jsii.Number(2),
		PublicLoadBalancer: jsii.Bool(true),
		DomainName:         jsii.String(props.DomainName),
		DomainZone:         hostedZone,
		Certificate:        certificate,
		RedirectHTTP:       jsii.Bool(true),
	})

	// Create Route53 record
	awsroute53.NewARecord(stack, jsii.String("AliasRecord"), &awsroute53.ARecordProps{
		Zone:       hostedZone,
		RecordName: jsii.String(props.DomainName),
		Target: awsroute53.RecordTarget_FromAlias(
			awsroute53targets.NewLoadBalancerTarget(fargateService.LoadBalancer()),
		),
	})

	// Output the repository URI
	awscdk.NewCfnOutput(stack, jsii.String("RepositoryUri"), &awscdk.CfnOutputProps{
		Value:       repository.RepositoryUri(),
		Description: jsii.String("ECR Repository URI for " + props.Stage),
	})

	// Output the service URL
	awscdk.NewCfnOutput(stack, jsii.String("ServiceUrl"), &awscdk.CfnOutputProps{
		Value:       jsii.String("https://" + props.DomainName),
		Description: jsii.String("Service URL for " + props.Stage),
	})

	return stack
}
