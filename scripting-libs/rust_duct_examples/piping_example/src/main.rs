use duct::cmd;

fn main() {
    cmd!("echo", "hi").pipe(cmd!("sed", "s/i/o/")).run().unwrap();
}
