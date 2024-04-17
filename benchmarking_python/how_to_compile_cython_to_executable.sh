#!/bin/bash

# to compile .pyx to .c

cython3 -3 --embed $1.pyx -o $1.c

# to compile .c to executable

gcc $1.c -o $1 $(pkg-config --libs --cflags python3) -I /usr/include/python3.5m/


