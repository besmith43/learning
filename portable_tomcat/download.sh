#!/usr/bin/env bash


os_name="$(uname)"
arch="$(uname -p)"

download_url=""


if [ "$os_name" == "Darwin" ] && [ "$arch" == "arm" ]; then
    download_url="https://download.oracle.com/java/25/latest/jdk-25_macos-aarch64_bin.tar.gz"
elif [ "$os_name" == "Darwin" ] && [ "$arch" == "x64" ]; then
    download_url="https://download.oracle.com/java/25/latest/jdk-25_macos-x64_bin.tar.gz"
elif [ "$os_name" == "Linux" ] && [ "$arch" == "arm" ]; then
    download_url="https://download.oracle.com/java/25/latest/jdk-25_macos-aarch64_bin.tar.gz"
elif [ "$os_name" == "Linux" ] && [ "$arch" == "x64" ]; then
    download_url="https://download.oracle.com/java/25/latest/jdk-25_macos-x64_bin.tar.gz"
else
    echo "this script doesn't support windows" >&2
    exit 1
fi

if [ ! -d jdk ]; then
    curl "$download_url" -o jdk.tar.gz

    tar -xvf jdk.tar.gz

    rm jdk.tar.gz

    java_output_dir="$(ls -td  jdk* | head -n 1)"

    mv "$java_output_dir" jdk
fi


if [ ! -d tomcat-11-home ]; then
    curl "https://dlcdn.apache.org/tomcat/tomcat-11/v11.0.22/bin/apache-tomcat-11.0.22.tar.gz" -o tomcat-11.tar.gz

    tar -xvf tomcat-11.tar.gz

    tomcat_output_dir="$(ls -td  *tomcat* | head -n 1)"

    mv "$tomcat_output_dir" tomcat-11-home
fi


if [ ! -d tomcat-instance/work ]; then
    mkdir tomcat-instance/work
fi


if [ ! -d tomcat-instance/logs ]; then
    mkdir tomcat-instance/logs
fi


if [ ! -d tomcat-instance/temp ]; then
    mkdir tomcat-instance/temp
fi











