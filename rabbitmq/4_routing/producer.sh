#!/usr/bin/env bash


figlet Running Producer
go run emit_log_direct.go
go run emit_log_direct.go error "Run. Run. Or it will explode!"


