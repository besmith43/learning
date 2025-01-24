use std::env;


/*
 * the current problem with this solution is that it is not taking into account that specific flags
 * can be passed in, and it doesn't process those flags
 */


fn main() {
    let a: Vec<String> = env::args().collect();

    // dbg!(&a);

    let mut sentence: String = a
        .iter()
        .skip(1)
        .fold(String::new(), |s1, s2| s1 + " " + s2)
        .trim_start()
        .trim_end()
        .to_string();

    // dbg!(&sentence);

    sentence = sentence.replace("\\a", r"\a");
    sentence = sentence.replace("\\b", r"\b");
    sentence = sentence.replace("\\f", r"\f");
    sentence = sentence.replace("\\n", "\n");
    sentence = sentence.replace("\\r", "\r");
    sentence = sentence.replace("\\t", "\t");
    sentence = sentence.replace("\\v", r"\v");

    // dbg!(&sentence);

    println!("{}", sentence);
}
