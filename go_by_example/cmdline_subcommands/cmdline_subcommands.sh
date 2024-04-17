#!/bin/bash

go build cmdline_subcommands.go

./cmdline_subcommands foo -enable -name=joe a1 a2
./cmdline_subcommands bar -level 8 a1
./cmdline_subcommands bar -enable a1


rm cmdline_subcommands
