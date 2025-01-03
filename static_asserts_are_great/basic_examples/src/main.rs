extern crate static_assertions as sa;

fn main() {
    let thingy = "testing";

    sa::assert_eq_size_val!(thingy, "testing");
    sa::assert_eq_size_val!(thingy, "test");

}
