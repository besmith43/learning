#!/usr/bin/env python3

num = 100000
isprime = False
prime_count = 1

for i in range(2,num):
    isprime = True
    for j in range(2,i):
        if (i % j == 0):
            isprime = False
            break

    if (isprime):
        prime_count += 1

print("The number of primes is ", prime_count)
