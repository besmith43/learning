use anyhow::Result;
use anyhow::Error;
use anyhow::anyhow;


fn main() {
    option();
    result();
}

fn option() {
    let answer = option_divide(2.0, 3.0);

    // rust's primary idiom is using the match statment so that the compiler can check it
    // however you can't have it let new_var = match unless you have an f64 for the None path
    match answer {
        Some(n) => println!("Match Answer: {}", n),
        None => println!("the denominator is 0"),
    };

    // this is why rust let's us handle it this way
    // and for Options, that's ok because it's either Some or None
    // a binary set of paths
    if let Some(n) = answer {
        println!("Some Answer: {}", n);
    }


    // the idea here being that go does if err != nil {}
    // I can say if let None = Option to exit early in a similar fashion
    if let None = answer {
        println!("the denominator was 0");
    } else if let Some(n) = answer {
        println!("Some Answer: {}", n);
    } else { // with the else if, this becomes unreachable
             // but is the intent more readable now?
        println!("it worked");
    }

    // or perhaps, I should do it this way
    if let None = answer {
        println!("the denominator was 0");
        std::process::exit(1);
    }

    let num = answer.unwrap();
    println!("Happy Path Answer: {}", num);
}


fn option_divide(numerator: f64, denominator: f64) -> Option<f64> {
    if denominator == 0.0 {
        None
    } else {
        Some(numerator/denominator)
    }
}


fn result() {
    let answer = result_divide(2.0, 3.0);

    match answer {
        Ok(n) => println!("Match Answer: {}", n),
        Error => println!("Match Division Failed"),
    };
}


fn result_divide(numerator: f64, denominator: f64) -> Result<f64> {
    if denominator == 0.0 {
        Err(anyhow!("you can't divide by 0"))
    } else {
        Ok(numerator/denominator)
    }
}



