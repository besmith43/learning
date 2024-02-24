#!/bin/bash

go build cmdline_args.go
./cmdline_args a b c d

rm cmdline_args
