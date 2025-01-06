when you create a lib based crate with `cargo init --lib`, it allows you to create a bin directory.
In that bin directory you can put as many rust files as you want.
Each one of these files will be need a main function.
This is one way to have a shared library across many programs.


To run the binaries in the src/bin, you'll need to use the following command: `cargo run --bin <file name wo .rs>`.
All the normal rules apply when importing from the lib.rs and it's modules.

You can almost think of this like Python's `if __name__ == __main__:` check for a library, but do keep in mind that the lib.rs is the main entry point for the library crate.
