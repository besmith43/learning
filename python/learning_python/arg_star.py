#!/usr/bin/env python3

"""
see examples @ https://pythontips.com/2013/08/04/args-and-kwargs-in-python-explained/
"""

def test_var_args(f_arg, *argv):
    print("first normal arg:", f_arg)
    for arg in argv:
        print("another arg through *argv :", arg)

test_var_args('yasoob','python','eggs','test')
