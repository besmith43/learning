#!/usr/bin/env bash

# note: computing fib takes awhile for anything above around 40
# 50 takes approximately 50 seconds and 100 took over 15 minutes before I quit
#
# also don't forget that the computation is being done on the server.  not by the client

figlet Running Producer
go run rpc_client.go 30
go run rpc_client.go 38
go run rpc_client.go 35
go run rpc_client.go 15
go run rpc_client.go 2
go run rpc_client.go 10

