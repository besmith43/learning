use std::env;
use std::fs::read_to_string;


fn main() {
    let a: Vec<String> = env::args().skip(1).collect();

    // dbg!(&a);

    let contents: Vec<String> = read_to_string(&a[1])
        .unwrap()
        .lines()
        .map(String::from)
        .collect();

    // dbg!(&contents);

    for line in contents {
        if line.contains(&a[0]) {
            println!("{}", line);
        }
    }
}
