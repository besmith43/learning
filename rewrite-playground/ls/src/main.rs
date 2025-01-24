use std::fs;
use std::env;


fn main() {
    let a: Vec<String> = env::args().skip(1).collect();

    if a.len() == 0 {
        list_dir("./");
    } else {
        list_dir(&a[0]);
    }
}


fn list_dir(input: &str) {
    let paths = fs::read_dir(input).unwrap();

    for path in paths {
        println!("{}", path.unwrap().path().display());
    }
}
