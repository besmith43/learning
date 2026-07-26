#!/usr/bin/env python


import sh


def main():
    print(sh.wc("-l", _in=sh.ls("main.py")))
    print(sh.wc("-l", "main.py"))
    print(sh.ls("-l", "/tmp", color="never"))


if __name__ == "__main__":
    main()
