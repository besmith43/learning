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


curl "$download_url" -o jdk.tar.gz


tar -xvf jdk.tar.gz

rm jdk.tar.gz

output_dir="$(ls -td  jdk* | head -n 1)"

mv "$output_dir" jdk


