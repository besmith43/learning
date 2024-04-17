use duct::cmd;
use std::io::prelude::*;
use std::io::BufReader;

fn main() {
    let big_cmd = cmd!("bash", "-c", "echo out && echo err 1>&2");
    let reader = big_cmd.stderr_to_stdout().reader().unwrap();
    let mut lines = BufReader::new(reader).lines();

    println!("{}", lines.next().unwrap().unwrap());
    println!("{}", lines.next().unwrap().unwrap());
}
