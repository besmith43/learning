#!/usr/bin/env python

"""
example cam from https://medium.com/@happymishra66/lambda-map-and-filter-in-python-4935f248593
"""

# basic setup for lambda function
# lambda arguments : expression

# addition example
def add(x, y):
    return x + y

# call function
add(2,3)

# lambda version

add = lambda x, y : x + y

print(add(2,3))

