#!/usr/bin/env bash

function check_last_command {
    if [ $1 -ne 0 ]; then
        echo "$2" >&2
        exit 1
    fi
}

if [ -f cdk ]; then
    rm cdk
fi

if [ -d cdk.out ]; then
    rm -r cdk.out
fi

cdk acknowledge 34892

check_last_command $? "cdk acknowledge 34892 command failed"

cdk bootstrap

check_last_command $? "cdk bootstrap command failed"

go build

check_last_command $? "go build command failed"

echo "cdk listing:" >&2
cdk list

# cdk synth

# check_last_command $? "cdk synth command failed"

# cdk deploy does have --require-approval with options like never, broadening
cdk deploy --yes

check_last_command $? "cdk deploy command failed"

