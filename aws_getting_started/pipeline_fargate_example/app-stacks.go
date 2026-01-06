package main

import (
	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/jsii-runtime-go"
)

// Create separate apps for each stage to deploy to different regions
func NewAlphaApp() awscdk.App {
	app := awscdk.NewApp(nil)

	NewFargateStack(app, "FargateAlphaStack", &FargateStackProps{
		StackProps: awscdk.StackProps{
			Env: &awscdk.Environment{
				Region: jsii.String("us-east-1"),
			},
		},
		Stage:      "alpha",
		DomainName: "alpha.besmithaws.click",
	})

	return app
}

func NewBetaApp() awscdk.App {
	app := awscdk.NewApp(nil)

	NewFargateStack(app, "FargateBetaStack", &FargateStackProps{
		StackProps: awscdk.StackProps{
			Env: &awscdk.Environment{
				Region: jsii.String("us-east-2"),
			},
		},
		Stage:      "beta",
		DomainName: "beta.besmithaws.click",
	})

	return app
}

func NewProdApp() awscdk.App {
	app := awscdk.NewApp(nil)

	NewFargateStack(app, "FargateProdStack", &FargateStackProps{
		StackProps: awscdk.StackProps{
			Env: &awscdk.Environment{
				Region: jsii.String("us-west-2"),
			},
		},
		Stage:      "prod",
		DomainName: "prod.besmithaws.click",
	})

	return app
}
