package main

import (
	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
)

type FargatePipelineStackProps struct {
	awscdk.StackProps
}

func NewFargatePipelineStack(scope constructs.Construct, id string, props *FargatePipelineStackProps) awscdk.Stack {
	var sprops awscdk.StackProps
	if props != nil {
		sprops = props.StackProps
	}
	stack := awscdk.NewStack(scope, &id, &sprops)

	// Pipeline stack will be defined here
	NewPipelineStack(stack, "Pipeline", &PipelineStackProps{})

	return stack
}

func main() {
	defer jsii.Close()

	app := awscdk.NewApp(nil)

	NewFargatePipelineStack(app, "FargatePipelineStack", &FargatePipelineStackProps{
		StackProps: awscdk.StackProps{
			Env: env(),
		},
	})

	app.Synth(nil)
}

func env() *awscdk.Environment {
	return &awscdk.Environment{
		Region: jsii.String("us-east-1"), // Pipeline will be in us-east-1
	}
}
