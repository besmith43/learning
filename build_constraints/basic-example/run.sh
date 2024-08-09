#!/bin/bash

########################################################
#
# this example came from stack overflow
# https://stackoverflow.com/questions/36703867/golang-preprocessor-like-c-style-compile-switch
#
# it makes reference to go contraints docs
# https://pkg.go.dev/go/build#hdr-Build_Constraints
#
# which tell you to run the following:
# go help buildconstraint
#
########################################################


# go run main.go

echo building A example
go build -o a_example -tags COMPILE_OPTION,DEBUG

echo running A example
./a_example

echo building B example
go build -o b_example

echo running B example
./b_example

echo cleaning up
rm a_example
rm b_example


