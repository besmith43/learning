fn main() {

    let a: String = "Bob".to_string();
    let b = "Bob";

    if a.eq(b) {
        println!("a.eq(b) true");
    }

    if b.eq(&a) {
        println!("b.eq(&a) true");
    }
}
