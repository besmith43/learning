#!/usr/bin/env python3

def fib(n):
    a, b = 0, 1
    for i in range(n):
        a, b = a + b, a
    return a

if __name__ == '__main__':
    import sys
    arg = int(sys.argv[1])
    num = fib(arg)
    print(num)
