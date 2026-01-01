#!/usr/bin/env bash

apt update

apt install curl xz-utils -y

# installing golang

if [ "$(uname -p)" == "aarch64" ]; then
	curl https://dl.google.com/go/go1.25.5.linux-arm64.tar.gz -o /tmp/go.tar.gz
else
	curl https://dl.google.com/go/go1.25.5.linux-amd64.tar.gz -o /tmp/go.tar.gz
fi 

tar -C /usr/local -xzf /tmp/go.tar.gz

echo "export PATH=\$PATH:/usr/local/go/bin" ~/.profile
export PATH=$PATH:/usr/local/go/bin


# installing zig

if [ "$(uname -p)" == "aarch64" ]; then
	curl https://ziglang.org/download/0.15.2/zig-aarch64-linux-0.15.2.tar.xz -o /tmp/zig.tar.xz
else
	curl https://ziglang.org/download/0.15.2/zig-x86_64-linux-0.15.2.tar.xz -o /tmp/zig.tar.xz
fi

tar -C /usr/local -xf /tmp/zig.tar.xz

if [ "$(uname -p)" == "aarch64" ]; then
	mv /usr/local/zig-aarch64-linux-0.15.2 /usr/local/zig
else
	mv /usr/local/zig-x86_64-linux-0.15.2 /usr/local/zig
fi

echo "export PATH=\$PATH:/usr/local/zig" ~/.profile
export PATH=$PATH:/usr/local/zig


# installing fzf

if [ "$(uname -p)" == "aarch64" ]; then
	curl https://github.com/junegunn/fzf/releases/download/v0.67.0/fzf-0.67.0-linux_arm64.tar.gz -o /tmp/fzf.tar.gz
else
	curl https://github.com/junegunn/fzf/releases/download/v0.67.0/fzf-0.67.0-linux_amd64.tar.gz -o /tmp/fzf.tar.gz
fi

tar -C /usr/local -xf /tmp/fzf.tar.gz



