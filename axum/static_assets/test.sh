#!/usr/bin/env bash


echo
echo

echo calling /
rh http://localhost:3000/

echo
echo

echo calling /assets/index.html
rh http://localhost:3000/assets/index.html
