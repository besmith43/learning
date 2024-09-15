#!/usr/bin/env bash


figlet Starting Consumer

# receive all messages
go run receive_logs_topic.go "*.critical"

