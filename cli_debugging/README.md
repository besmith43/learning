# Getting Started with Delve for Golang Debugging

1. Make sure that golang and delve are installed and available in your system path

```bash
    which go
    which dlv
```

2. set your working directory to the root of your project

3. run the following command but modify it for your project

```bash
    dlv debug ./cmd/hello/
```

NOTE: the path is to where the source code is

4. now that you have dlv loaded up with a freshly compiled copy of your code, set a breakpoint at the start of the main program

```
    b main.main
```

or you can set a breakpoint at a specific line

```
    b 24
```

5. then start the program

```
    c
```

NOTE: c is an alias for continue

6. you can now step through your code's execution with the next command like this

```
    n
```

7. you can also print out the value of variables with the print command

```
    p i
```

for more info see [here](https://www.jamessturtevant.com/posts/Using-the-Go-Delve-Debugger-from-the-command-line/)

