


const RED: &'static str = "\x1b[31m";
const GREEN: &'static str = "\x1b[32m";
const RESET: &'static str = "\x1b[0m";



fn main() {
    println!("{}Hello, {}world{}!", RED, GREEN, RESET);
}

