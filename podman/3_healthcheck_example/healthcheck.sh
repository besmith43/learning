#!/usr/bin/env bash


echo $(date +%Y%m%d-%H%M%S) - running healthcheck >> /var/logs/healthcheck.log

if pgrep -x "main" > /dev/null; then
	echo $(date +%Y%m%d-%H%M%S) - main executable stopped >> /var/logs/healthcheck.log
	exit 0
else
	echo $(date +%Y%m%d-%H%M%S) - main executable still running >> /var/logs/healthcheck.log
	exit 1
fi
