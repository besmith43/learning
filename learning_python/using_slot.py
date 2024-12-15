#!/usr/bin/env python

"""
This is a trick to assist with memory management that will allow you to use significantly less if you don't want to give your objects addition properties dynamically.
Also use del to remove ref counter to object when done using object

see here for more info
https://tech.oyster.com/save-ram-with-python-slots/
"""

# explain of the dict mutable version
class Dog():
    pass

buddy = Dog()
buddy.name = 'Buddy'

print("dog objects dict")
print(buddy.__dict__)

class Point():
    __slots__ = ('x', 'y')

point = Point()
point.x = 5
point.y = 7

import sys

print("size of Dog object instance")
print(sys.getsizeof(buddy))
print("size of the Point object instance")
print(sys.getsizeof(point))

# this will fail
# point.name = "Fred"

print("size of dicts")
print(sys.getsizeof(dict()))
print("size of tuples")
print(sys.getsizeof(tuple()))