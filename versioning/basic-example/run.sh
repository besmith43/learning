#!/bin/bash

# this got pulled from the following reddit post:
# https://www.reddit.com/r/golang/comments/4cpi2y/question_where_to_keep_the_version_number_of_a_go/?rdt=64406

go run -ldflags="-X main.version=$(cat version.txt)" *.go

