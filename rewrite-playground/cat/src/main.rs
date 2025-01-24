use std::env;
use std::fs::File;
use std::io::Read;


fn main() {
    let a: Vec<String> = env::args().skip(1).collect();

    dbg!(&a);

    // let b: Vec<File> = Vec::new();

    for item in a {
        let mut file = File::open(item).unwrap();
        let mut contents = String::new();  
        file.read_to_string(&mut contents).unwrap();

        // remove the last new line because println! will add it back
        // let mut c = contents.chars();
        // c.next_back();
        // let final_contents = c.as_str();

        // println!("{}", final_contents);

        // or...
        print!("{}", contents);
    }
}
