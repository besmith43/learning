use std::process::{Command, Stdio};

fn main() {
    let gum_choice = Command::new("gum")
        .stdin(Stdio::inherit())
        .stdout(Stdio::inherit())
        .stderr(Stdio::inherit())
        .arg("choose")
        .arg("file1")
        .arg("file2")
        .arg("file3")
        .arg("file4")
        .arg("file5")
        // .spawn() // spawn should inherit stdin, stdout, and stderr just like status, but it's not
                 // working
        // .status() // status allows gum to work however I can't get anything but the exit code
        .output() // as long as stdin, stdout, and stderr are explicity set to inherit, output
                  // works exactly as I'd like
        .expect("gum choose command failed to start");

    // dbg!(gum_choice);

    let stdout: String = gum_choice.stdout.iter().map(|d| *d as char).collect();

    println!("{}", stdout);

    println!("spawn");
    Command::new("ls")
        .spawn()
        .expect("ls command failed to start");

    println!("output");
    Command::new("ls")
        .output()
        .expect("ls command failed to start");

    println!("status");
    Command::new("ls")
        .status()
        .expect("ls command failed to start");

    let listing = Command::new("ls")
        .status()
        .expect("ls command failed to start");

    println!("debug output");
    dbg!(listing); // this runs in debug and release mode
                   // also it outputs to stderr
}
