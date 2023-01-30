use rscripter::*;

fn main() {
    log!("Hello World");
    log!(echo("Hello World"));
    cmd!("ping", "-c", "3", "1.1.1.1");
}
