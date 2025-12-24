#!/usr/bin/env bash


cd server

./run.sh &


cd ../client


./run.sh


kill %1

