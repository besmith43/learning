#!/usr/bin/env bash

function check_last_command {
    if [ $1 -ne 0 ]; then
        echo "$2" >&2
        exit 1
    fi
}

if [ -f cdk ]; then
    echo "removing cdk binary" >&2
    rm cdk
fi

if [ -d cdk.out ]; then
    echo "removing cdk.out directory" >&2
    rm -r cdk.out
fi

echo "running cdk acknowledge" >&2
cdk acknowledge 34892 --profile personal

check_last_command $? "cdk acknowledge 34892 command failed"

echo "running cdk bootstrap" >&2
cdk bootstrap --profile personal

check_last_command $? "cdk bootstrap command failed"

echo "running go build" >&2
go build

check_last_command $? "go build command failed"

echo "cdk listing:" >&2
cdk list --profile personal

# cdk synth --profile personal

# check_last_command $? "cdk synth command failed"

echo "running cdk deploy" >&2
# cdk deploy does have --require-approval with options like never, broadening
cdk deploy --yes --profile personal

check_last_command $? "cdk deploy command failed"

