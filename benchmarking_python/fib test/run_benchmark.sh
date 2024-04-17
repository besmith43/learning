#!/bin/bash

echo "Compiled C"
time ./fib_c $1

echo "Interpreted Python"
time ./fib.py $1

echo "Compiled Python (pyinstaller)"
time ./fib_py $1

echo "Compiled Cython"
time ./fib_cy $1

echo "Compiled Nuitka"
time ./fib.dist/fib.exe $1

echo "cx_freeze executable"
time ./build/exe.linux-x86_64-3.5/fib $1
