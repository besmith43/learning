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


    // NOTE: fzf can only get choices from piped in data
    // so.. we're gonna set it up with an echo command first
    // got the idea for this from here: https://stackoverflow.com/questions/73469520/how-to-pipe-commands-in-rust
    let data = Command::new("echo")
        .arg("file1\nfile2\nfile3\nfile4\nfile5\n")
        .stdout(Stdio::piped())
        .spawn()
        .unwrap();

    let fzf_choice = Command::new("fzf")
        .stdin(Stdio::from(data.stdout.unwrap()))
        .stdout(Stdio::inherit())
        .stderr(Stdio::inherit())
        .output()
        .expect("gum choose command failed to start");

    let fzf_stdout: String = fzf_choice.stdout.iter().map(|d| *d as char).collect();

    println!("{}", fzf_stdout);




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
