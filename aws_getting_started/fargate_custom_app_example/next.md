# TODO

- attempt deployment against localstack as the aws dashboard is timing out with the following error:

CdkGoAppStack: creating CloudFormation changeset...
9:55:27 PM | CREATE_FAILED        | AWS::ECS::Service                         | FargateServiceECC8084D
Resource handler returned message: "Exceeded attempts to wait" (RequestToken: e3e41999-0a10-576e-81fd-56b7cf8b80ea, Handl
erErrorCode: NotStabilized)

❌  CdkGoAppStack failed: ToolkitError: The stack named CdkGoAppStack failed creation, it may need to be manually deleted from the AWS console: ROLLBACK_COMPLETE: Resource handler returned message: "Exceeded attempts to wait" (RequestToken: e3e41999-0a10-576e-81fd-56b7cf8b80ea, HandlerErrorCode: NotStabilized)

real	186m27.015s
user	2m47.435s
sys	0m43.793s
cdk deploy command failed




