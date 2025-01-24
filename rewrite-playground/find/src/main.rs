use std::env;
use std::fs;

// recursively find main.rs

fn main() {
    let a: Vec<String> = env::args().skip(1).collect();

    dbg!(&a);

    find(&a[0], "./");
}

fn find(input: &str, starting_dir: &str) {
    let paths = fs::read_dir(starting_dir).unwrap();


    for path in paths {
        let path = path.unwrap();
        if path.file_name() == input {
            println!("{}", path.path().display());
            continue;
        }

        if path.file_type().unwrap().is_dir() {
            find(&input, &path.path().display().to_string());
        }
    }
}

