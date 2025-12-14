#!/usr/bin/env bash


dnsName="$(aws elbv2 describe-load-balancers --profile personal --query 'LoadBalancers[0].DNSName' --output text)"

curl http://$dnsName

exit 0

fargateCluster="$(aws ecs list-clusters --profile personal --query 'clusterArns[]' --output text)"

if [ -z "$fargateCluster" ]; then
    echo "getting the fargate cluster failed" >&2
    exit 1
fi

fargateService="$(aws ecs list-services --cluster $fargateCluster --profile personal --query 'serviceArns[]' --output text)"

if [ -z "$fargateService" ]; then
    echo "getting the fargate service failed" >&2
    exit 1
fi

aws ecs describe-services --cluster $fargateCluster --services $fargateService --profile personal
exit

fargateLBArns="$(aws ecs describe-services --cluster $fargateCluster --services $fargateService --profile personal --query "services[0].loadBalancers[0].targetGroupArn" --output text)"

if [ -z "$fargateLBArns" ]; then
    echo "getting the fargate load balancer arns failed" >&2
    exit 1
fi

aws elbv2 describe-load-balancers --names $fargateLBArns --profile personal --query "LoadBalancers[0].DNSName" --output text

# curl "$fargateUrl"

