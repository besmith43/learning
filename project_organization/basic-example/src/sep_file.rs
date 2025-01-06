
// needs pub
pub fn seperate_func() {
    println!("hello from sep_file!");
    priv_func();
}

fn priv_func() {
    println!("hello from sep_file's private function!");
}
