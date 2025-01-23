use std::process::Command;


fn main() {
    let output = Command::new("ssh").args(["nas", "ls", "-al"])
        .output()
        .expect("ssh failed");

    let stdout: String = output.stdout.iter().map(|d| *d as char).collect();

    println!("{}", stdout);
}
