#!/bin/bash

# compile

# compile c program
gcc ./find_primes.c -o find_primes

# compile pyinstaller
pyinstaller -w -F ./find_primes.py

# compile nuitka
nuitka --standalone ./find_primes.py

# compile cx_freeze
python3 setup.py build_exe

# compile cython
cython3 -3 --embed find_primes.pyx -o find_primes_cython.c

gcc find_primes_cython.c -o find_primes_cython $(pkg-config --libs --cflags python3) -I /usr/include/python3.5m/

# run

echo "Compiled C"
time ./find_primes

echo "Interpreted Python"
/usr/bin/time -f "%e" ./find_primes.py

echo "Compiled Python (pyinstaller)"
/usr/bin/time -f "%e" ./dist/find_primes

echo "Compiled Python Nuitka"
/usr/bin/time -f "%e" ./find_primes.dist/find_primes.exe

echo "Compiled Python CX_Freeze"
/usr/bin/time -f "%e" ./build/exe.linux-x86_64-3.5/find_primes

echo "Compiled Cython"
/usr/bin/time -f "%e" ./find_primes_cython
