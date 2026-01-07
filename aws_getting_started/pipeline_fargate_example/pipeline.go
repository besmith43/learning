package main

import (
	"github.com/aws/aws-cdk-go/awscdk/v2"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscodebuild"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscodecommit"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscodepipeline"
	"github.com/aws/aws-cdk-go/awscdk/v2/awscodepipelineactions"
	"github.com/aws/aws-cdk-go/awscdk/v2/awsiam"
	"github.com/aws/constructs-go/constructs/v10"
	"github.com/aws/jsii-runtime-go"
)

type PipelineStackProps struct {
	awscdk.StackProps
}

func NewPipelineStack(scope constructs.Construct, id string, props *PipelineStackProps) constructs.Construct {
	construct := constructs.NewConstruct(scope, &id)

	// Create CodeCommit repository
	repo := awscodecommit.NewRepository(construct, jsii.String("FargateRepo"), &awscodecommit.RepositoryProps{
		RepositoryName: jsii.String("fargate-app"),
		Description:    jsii.String("Repository for Fargate application"),
	})

	// Create build project
	buildProject := awscodebuild.NewProject(construct, jsii.String("BuildProject"), &awscodebuild.ProjectProps{
		ProjectName: jsii.String("fargate-build"),
		Source: awscodebuild.Source_CodeCommit(&awscodebuild.CodeCommitSourceProps{
			Repository: repo,
		}),
		Environment: &awscodebuild.BuildEnvironment{
			BuildImage: awscodebuild.LinuxBuildImage_STANDARD_7_0(),
			Privileged: jsii.Bool(true), // Required for Docker builds
		},
		BuildSpec: awscodebuild.BuildSpec_FromObject(&map[string]interface{}{
			"version": "0.2",
			"phases": map[string]interface{}{
				"pre_build": map[string]interface{}{
					"commands": []string{
						"echo Logging in to Amazon ECR...",
						"aws ecr get-login-password --region $AWS_DEFAULT_REGION | docker login --username AWS --password-stdin $AWS_ACCOUNT_ID.dkr.ecr.$AWS_DEFAULT_REGION.amazonaws.com",
					},
				},
				"build": map[string]interface{}{
					"commands": []string{
						"echo Build started on `date`",
						"echo Building the Docker image...",
						"docker build -t $IMAGE_REPO_NAME:$IMAGE_TAG .",
						"docker tag $IMAGE_REPO_NAME:$IMAGE_TAG $AWS_ACCOUNT_ID.dkr.ecr.$AWS_DEFAULT_REGION.amazonaws.com/$IMAGE_REPO_NAME:$IMAGE_TAG",
					},
				},
				"post_build": map[string]interface{}{
					"commands": []string{
						"echo Build completed on `date`",
						"echo Pushing the Docker image...",
						"docker push $AWS_ACCOUNT_ID.dkr.ecr.$AWS_DEFAULT_REGION.amazonaws.com/$IMAGE_REPO_NAME:$IMAGE_TAG",
					},
				},
			},
		}),
	})

	// Add ECR permissions to build project
	buildProject.AddToRolePolicy(awsiam.NewPolicyStatement(&awsiam.PolicyStatementProps{
		Effect: awsiam.Effect_ALLOW,
		Actions: &[]*string{
			jsii.String("ecr:BatchCheckLayerAvailability"),
			jsii.String("ecr:GetDownloadUrlForLayer"),
			jsii.String("ecr:BatchGetImage"),
			jsii.String("ecr:GetAuthorizationToken"),
			jsii.String("ecr:PutImage"),
			jsii.String("ecr:InitiateLayerUpload"),
			jsii.String("ecr:UploadLayerPart"),
			jsii.String("ecr:CompleteLayerUpload"),
		},
		Resources: &[]*string{jsii.String("*")},
	}))

	// Create artifacts
	sourceOutput := awscodepipeline.NewArtifact(jsii.String("SourceOutput"))
	buildOutput := awscodepipeline.NewArtifact(jsii.String("BuildOutput"))

	// Create pipeline
	pipeline := awscodepipeline.NewPipeline(construct, jsii.String("FargatePipeline"), &awscodepipeline.PipelineProps{
		PipelineName: jsii.String("fargate-pipeline"),
		Stages: &[]*awscodepipeline.StageProps{
			{
				StageName: jsii.String("Source"),
				Actions: &[]awscodepipeline.IAction{
					awscodepipelineactions.NewCodeCommitSourceAction(&awscodepipelineactions.CodeCommitSourceActionProps{
						ActionName: jsii.String("Source"),
						Repository: repo,
						Output:     sourceOutput,
						Branch:     jsii.String("main"),
					}),
				},
			},
			{
				StageName: jsii.String("Build"),
				Actions: &[]awscodepipeline.IAction{
					awscodepipelineactions.NewCodeBuildAction(&awscodepipelineactions.CodeBuildActionProps{
						ActionName: jsii.String("Build"),
						Project:    buildProject,
						Input:      sourceOutput,
						Outputs:    &[]awscodepipeline.Artifact{buildOutput},
					}),
				},
			},
			{
				StageName: jsii.String("DeployAlpha"),
				Actions: &[]awscodepipeline.IAction{
					awscodepipelineactions.NewCloudFormationCreateUpdateStackAction(&awscodepipelineactions.CloudFormationCreateUpdateStackActionProps{
						ActionName:       jsii.String("DeployAlpha"),
						StackName:        jsii.String("fargate-alpha"),
						TemplatePath:     buildOutput.AtPath(jsii.String("alpha-template.yaml")),
						AdminPermissions: jsii.Bool(true),
						Region:           jsii.String("us-east-1"),
					}),
				},
			},
			{
				StageName: jsii.String("DeployBeta"),
				Actions: &[]awscodepipeline.IAction{
					awscodepipelineactions.NewCloudFormationCreateUpdateStackAction(&awscodepipelineactions.CloudFormationCreateUpdateStackActionProps{
						ActionName:       jsii.String("DeployBeta"),
						StackName:        jsii.String("fargate-beta"),
						TemplatePath:     buildOutput.AtPath(jsii.String("beta-template.yaml")),
						AdminPermissions: jsii.Bool(true),
						Region:           jsii.String("us-east-2"),
					}),
				},
			},
			{
				StageName: jsii.String("DeployProd"),
				Actions: &[]awscodepipeline.IAction{
					awscodepipelineactions.NewCloudFormationCreateUpdateStackAction(&awscodepipelineactions.CloudFormationCreateUpdateStackActionProps{
						ActionName:       jsii.String("DeployProd"),
						StackName:        jsii.String("fargate-prod"),
						TemplatePath:     buildOutput.AtPath(jsii.String("prod-template.yaml")),
						AdminPermissions: jsii.Bool(true),
						Region:           jsii.String("us-west-2"),
					}),
				},
			},
		},
	})

	construct.Node().AddDependency(pipeline)
	return construct
}
