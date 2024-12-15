#!/usr/bin/env python3

import argparse
import sys

"""
Python Docs article from Python Docs at https://docs.python.org/3.6/library/argparse.html
parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('integers', metavar='N', type=int, nargs='+',
                    help='an integer for the accumulator')
parser.add_argument('--sum', dest='accumulate', action='store_const',
                    const=sum, default=max,
                    help='sum the integers (default: find the max)')

args = parser.parse_args()
print(args.accumulate(args.integers))
"""


arg_length = len(sys.argv)
print(sys.argv)
print(arg_length)

# at a minimum need this line and call .parse_args()
parser = argparse.ArgumentParser(prog='Program Name', description='Using_ArgParse is a program to test and produce example code for using the python package argparse')
parser.add_argument('--x', type=int)
parser.add_argument('--y', type=int)
parser.add_argument('--z')
parser.add_argument('-f', "--random_function", action='store_true', help="test a flag for function") # switch statement
parser.add_argument('-v', '--version', action='version', version='%(prog)s 1.0')

args = parser.parse_args()


# test is specific arg was passed
if args.x != None:
    # to get the value of a command line value
    print(args.x)

if args.random_function:
    print(args.random_function)

if args.z != None:
    print(args.z)


