#!/bin/bash

echo "Compiled C"
time ./hello_c

echo "Interpreted Python"
time ./hello.py

echo "Compiled Python"
time ./hello_py

echo "Compiled Cython"
time ./hello_cython
