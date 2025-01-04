use std::io;
use std::env;
use std::io::BufRead;

fn main() -> io::Result<()> {

    let args: Vec<String> = env::args().collect();

    for arg in args {
        eprintln!("{}", arg);
    }

    let mut buffer = vec![];
    // let mut buffer = String::new();
    let stdin = io::stdin(); // We get `Stdin` here.
    let mut handle = stdin.lock();
    let _ = handle.read_until(b'\0', &mut buffer); // I'm guessing that the b'\0' is the null byte
                                                   // or EOF
    println!("{}", String::from_utf8(buffer).unwrap());

    println!("Hello, stdout!");
    eprintln!("Hello, stderr!");

    Ok(())
}
