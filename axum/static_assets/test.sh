#!/usr/bin/env bash


echo
echo

echo calling /
rh -v http://localhost:3000/

echo
echo

echo calling /assets/index.html
rh -v http://localhost:3000/assets/index.html
