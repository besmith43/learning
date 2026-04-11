#!/usr/bin/env bash


if [ -f .build-scripts/setup-env.sh ]; then
    source .build-scripts/setup-env.sh
else
    echo "setup env script missing" >&2
    exit 1
fi

if [ -z "$CATALINA_HOME" ] || [ -z "$CATALINA_BASE" ]; then
    echo "catalina home or base is empty" >&2
    exit 1
fi


echo "stop tomcat"
echo "$CATALINA_BASE"
$CATALINA_BASE/bin/test.sh stop

