#!/usr/bin/env python


from pyfzf.pyfzf import FzfPrompt


def main():
    fzf = FzfPrompt()
    choice = fzf.prompt(range(0,10))
    print(choice)

if __name__ == "__main__":
    main()

