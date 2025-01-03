fn main() {
    assert!(1 == 1);
    assert!(1 == 2, "you can give a custom failure message, but the program will only panic on failure");
}
