#!/usr/bin/env bash

dotnet publish -c Release -p:PublishSingleFile=true -r linux-x64

scp -O bin/Release/net8.0/linux-x64/publish/basic-example nas:~/bin

ssh nas /var/services/homes/besmith/bin/basic-example

ssh nas rm /var/services/homes/besmith/bin/basic-example

