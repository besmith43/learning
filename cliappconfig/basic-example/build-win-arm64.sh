#!/usr/bin/env bash

dotnet publish -c Release -p:PublishSingleFile=true -r win-arm64
