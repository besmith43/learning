#!/usr/bin/env python3


import sys

def calculate_fib(num: int):

    if num == 1:
        return 0

    if num == 2:
        return 1

    x = 1
    y = 1
    sum = 0

    for i in range(2, num, 1):
        sum = x + y 
        x = y
        y = sum

    return sum

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("you need to pass in a number")


    x = int(sys.argv[1])

    answer = calculate_fib(x)
    
    print(answer)
