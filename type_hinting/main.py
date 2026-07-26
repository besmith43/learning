#!/usr/bin/env python


def greeting(name: str) -> None:
    print("Hello " + name)


def noHints(input):
    print(input)


if __name__ == "__main__":
    greeting("bob")
    noHints("no idea")

