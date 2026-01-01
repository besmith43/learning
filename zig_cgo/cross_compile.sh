#!/usr/bin/env bash


if [ -z "$(which fzf)" ] || [ -z "$(which zig)" ] || [ -z "$(which go)" ]; then
    echo "you are missing dependencies" >&2
    echo "please make sure that the following are installed and available in your path" >&2
    echo "fzf" >&2
    echo "zig" >&2
    echo "go" >&2
    exit 1
fi

if [ ! -f go.mod ]; then
    go mod init example.com
fi

if [ -f main ]; then
    rm main
fi


os="$(echo -e "darwin\nlinux\nwindows" | fzf --prompt="select os> ")"

arch="$(echo -e "arm\nx86" | fzf --prompt="select cpu arch> ")"


cc=""

if [ $os == "darwin" ] && [ $arch == "x86" ]; then
    cc="zig cc -target x86_64-macos"
elif [ $os == "darwin" ] && [ $arch == "arm" ]; then
    cc="zig cc -target aarch64-macos"
elif [ $os == "linux" ] && [ $arch == "x86" ]; then
    cc="zig cc -target x86_64-linux-musl"
elif [ $os == "linux" ] && [ $arch == "arm" ]; then
    cc="zig cc -target aarch64-linux-musl"
elif [ $os == "windows" ] && [ $arch == "x86" ]; then
    cc="zig cc -target x86_64-windows-gnu"
elif [ $os == "windows" ] && [ $arch == "arm" ]; then
    cc="zig cc -target aarch64-windows-gnu"
else
    echo "I'm sorry but something went wrong with your selections" >&2
    exit 1
fi


go mod tidy

CGO_ENABLED=1 GOOS=$os GOARCH=$arch CC=$cc go build -o main

if [ $? -eq 0 ]; then
    file main
fi

