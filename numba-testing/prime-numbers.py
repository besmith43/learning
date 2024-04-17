#!/usr/bin/env python3

from numba import njit
import time

def python_primes(total):
    primes = [1,2]
    for i in range(3,total):
        if (i%2) != 0:
            isprime = True
            for prime in primes:
                if prime != 1:
                    if (i%prime) == 0:
                        isprime = False
            if isprime:
                primes.append(i)

@njit
def numba_primes(total):
    primes = [1,2]
    for i in range(3,total):
        if (i%2) != 0:
            isprime = True
            for prime in primes:
                if prime != 1:
                    if (i%prime) == 0:
                        isprime = False
            if isprime:
                primes.append(i)

def main():
    number = 1000
    print("running python function")
    loop = 0
    t_end = time.time() + 10
    while time.time() < t_end:
        python_primes(number)
        loop = loop + 1
    print(f"Pure Python Function looped {loop} times in 10 seconds")
    print("running numba function")
    loop = 0
    t_end = time.time() + 10
    while time.time() < t_end:
        numba_primes(number)
        loop = loop + 1
    print(f"Numba Function looped {loop} times in 10 seconds")

if __name__ == "__main__":
    main()
