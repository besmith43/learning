#!/usr/bin/env bash


# https://www.nuget.org/packages/dotnet-t4

t4 powers.tt -c MyApp.Powers

cat powers.cs

t4 powers.tt -p:Max=6

glow powers.md

