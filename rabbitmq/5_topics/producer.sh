#!/usr/bin/env bash


figlet Running Producer
go run emit_log_topic.go
go run emit_log_topic.go error "Run. Run. Or it will explode!"
go run emit_log_topic.go "kern.critical" "A critical kernel error"
go run emit_log_topic.go "other.critical" "a critical non-kernel error"


