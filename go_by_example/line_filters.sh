#!/bin/bash

echo 'hello' > /tmp/lines
echo 'filter' >> /tmp/lines

cat /tmp/lines | go run line_filters.go

rm /tmp/lines
