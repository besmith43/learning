#!/usr/bin/env bash


dotnet publish -c Release --self-contained=true -p:PublishSingleFile=true

if [ -z "$(which dotnet-init)" ]; then
    cp ./bin/Release/net9.0/osx-arm64/publish/dotnet-init ~/.dotnet/tools/
fi

