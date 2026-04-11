#!/usr/bin/env bash


warfile="Hello.war"

if [ -d .tomcat-instance ]; then
    export CATALINA_BASE="$(pwd)/.tomcat-instance"
    echo $CATALINA_BASE
else
    echo ".tomcat-instance directory not found"
    exit 1
fi


if [ -d .tomcat-core ]; then
    export CATALINA_HOME="$(pwd)/.tomcat-core"
    echo $CATALINA_HOME
else
    echo ".tomcat-core directory not found"
    exit 1
fi

