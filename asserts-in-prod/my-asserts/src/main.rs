// use std::error::Error;


fn main() {
    let test = || -> bool { 1 == 2 };
    match assert(&test) {
        Ok(()) => println!("Everything's fine"),
        Err(e) => println!("The check failed: {e:?}"),
    };
}

pub fn assert(f: &dyn Fn() -> bool) -> Result<(), &'static str> {
    let result = f();

    if result {
        return Ok(());
    } else {
        return Err("failed");
    }
}

