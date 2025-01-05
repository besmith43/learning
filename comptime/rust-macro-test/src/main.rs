
macro_rules! comp {
    () => {
        chrono::Utc::now().format("%Y-%m-%d %H:%M").to_string()
    };
}



fn main() {
    println!("Hello, world!");
    println!("the compile time was {}", comp!());
}
