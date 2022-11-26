cdef int num = 100000
cdef int isprime = 0 # 0 for true, and 1 for false
prime_count = 1

for i in range(2,num):
    isprime = 0
    for j in range(2,i):
        if (i % j ==0):
            isprime = 1
            break

    if (isprime == 0):
        prime_count += 1

print("The number of primes is ", prime_count)
