package main

import (
	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsec2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecs"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsecspatterns"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
)

type FargateStackProps struct {
	awscdk.StackProps
}

func NewFargateStack(scope constructs.Construct, id string, props *FargateStackProps) awscdk.Stack {
	stack := awscdk.NewStack(scope, &id, &props.StackProps)

	vpc := awsec2.NewVpc(stack, jsii.String("Vpc"), &awsec2.VpcProps{
		MaxAzs: jsii.Number(2),
	})

	cluster := awsecs.NewCluster(stack, jsii.String("Cluster"), &awsecs.ClusterProps{
		Vpc: vpc,
	})

	awsecspatterns.NewApplicationLoadBalancedFargateService(stack, jsii.String("FargateService"), &awsecspatterns.ApplicationLoadBalancedFargateServiceProps{
		Cluster:      cluster,
		DesiredCount: jsii.Number(2),
		TaskImageOptions: &awsecspatterns.ApplicationLoadBalancedTaskImageOptions{
			Image: awsecs.ContainerImage_FromRegistry(jsii.String("amazon/amazon-ecs-sample"), nil),
		},
		PublicLoadBalancer: jsii.Bool(true),
		LoadBalancerName:   jsii.String("FargateLB"),
	})

	return stack
}

func main() {
	app := awscdk.NewApp(nil)

	NewFargateStack(app, "FargateStack", &FargateStackProps{
		awscdk.StackProps{
			Env: env(),
		},
	})

	app.Synth(nil)
}

func env() *awscdk.Environment {
	return nil
}
