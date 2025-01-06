use cmd_lib::*;
use std::env;
// use chrono::prelude::*;
use std::process::Command;

fn main() {
    let msg = "I love rust";

    run_cmd!(echo $msg).unwrap();
    run_cmd!(echo "This is the message: $msg").unwrap();

    // this doesn't work either
    //let time_now = Utc::now();
    //run_cmd!(echo "$time_now - logging" >> log.txt).unwrap();

    // control flow tests
    let file = format!("{}/.bashrc", env::var("HOME").unwrap().to_string());
    let result = run_fun!(cat ${file} | wc -l).unwrap();

    println!("{:?}", result);

    if result.trim().parse::<i32>().unwrap() >= 5 {
        println!("bashrc is huge!");
    }

    /*
    // this fails to work because gum throws an error when it
    // can't get control over the terminal
    // also run_cmd! doesn't handle &&
    if run_cmd!(gum confirm "commit changes?").is_ok() {
        run_cmd!(echo "you said yes").unwrap();
    }
    */

    // status let's the child process inheritant stdout, stdin, and stderr from the rust process
    let output = Command::new("gum")
                                    .arg("confirm")
                                    .arg("\"commit changes?\"")
                                    .status()
                                    .expect("failed to execute command");

    println!("{:?}", output);

    /*
    // this also fails because this library is designed for handling single line commands and using rust for logic and control flow
    run_cmd!(
        if [ -d ~/Bin ]; then
            echo "bin directory exists"
        fi
    ).unwrap();
    */
}
