#!/bin/zsh

# need to run m4 first
# m4 rules.m4 main.go.m4 > main.go
# m4 rules.m4 *.go.m4 # how to output that correctly?

for file in $(ls *.go.m4);
do
	m4 rules.m4 $file > ${file:0:-3}
done


#######################################
#
#  from what I can see,
#  this is a 1-to-1 mapping
#
#  need to test this with a wildcard
#
#######################################


if [ -f main.go ]; then
	go run *.go
else
	echo no main.go file was found
fi

