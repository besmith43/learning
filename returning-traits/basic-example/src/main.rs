// the idea for this example came from https://dev.to/francescoxx/what-are-traits-in-rust-a-well-known-concept-you-might-already-know-4b01


fn main() {

    let fb = Facebook {};

    notify(&fb);

    let x = returns_summariable();

    notify(&x);
}

struct Facebook {

}

impl Summary for Facebook {
    fn summarize(&self) -> String {
        String::from("summary from Facebook")
    }
}


struct Twitter {

}

impl Summary for Twitter {
    fn summarize(&self) -> String {
        String::from("summary from Twitter")
    }
}

trait Summary {
    fn summarize(&self) -> String {
        String::from("base summary")
    }
}


fn notify(service: &impl Summary) {
    println!("{}", service.summarize());
}


fn returns_summariable() -> impl Summary {
    Twitter {}
}


/*
fn returns_summariable_option(choice: &str) -> impl Summary {

    // this doesn't work because it decides that Twitter and Facebook aren't the same return type
    // using the match statement does the same thing
    // and you can't use impl Summary as a variable type declaration
    
    match choice {
        "twitter" => Twitter {},
        "facebook" => Facebook {},
        _ => Twitter {},
    }


    if choice == "twitter" {
        Twitter {}
    } else if choice == "facebook" {
        Facebook {}
    } else {
        Twitter {}
    }


    let fb = Facebook {};
    let x = Twitter {};
    
    if choice == "facebook" {
        fb
    } else {
        x
    }
}
*/

