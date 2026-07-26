#!/usr/bin/env python

# from plumbum import local
from plumbum.cmd import grep, ls


def main():
    chain = ls["-al"] | grep["main"]
    print("pipline chain: ", end="")
    print(chain)
    print("output of pipline: " + chain())


if __name__ == "__main__":
    main()
