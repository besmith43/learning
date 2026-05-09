#!/usr/bin/env bash


command=""


if [ -z "$1" ]; then
    echo "you need to pass start, status or stop" >&2
    exit 1
elif [ "$1" == "start" ] || [ "$1" == "status" ] || [ "$1" == "stop" ]; then
    command="$1"
else
    echo "unsupported command" >&2
    echo "only use start, status, or stop" >&2
    exit 1
fi


if [ "$(uname)" == "Darwin" ]; then
    export JAVA_HOME="jdk/Contents/Home"
elif [ "$(uname)" == "Linux" ]; then
    export JAVA_HOME="jdk"
else
    echo "os not supported" >&2
    exit 1
fi

export PATH="$JAVA_HOME/bin:$PATH"

printf "java path: "

which java


export CATALINA_HOME="$(pwd)/tomcat-11-home"


export CATALINA_BASE="$(pwd)/tomcat-instance"


$CATALINA_BASE/bin/test.sh "$command"

