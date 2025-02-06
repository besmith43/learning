using lldb because gdb doesn't work on aarch64


### Getting Started


1. build the binary and run the `rust-lldb` debugger

```bash
    cargo build
    rust-lldb ./target/debug/example
```

2. set a breakpoint

```
    b main
```

3. start the application


```
    run
```

4. debug

    From here you can do all the normal things that you'd expect like continue, step, print, etc.
    Just remember that q or quit will exit the debugger, and help will give you a help splash screen.



