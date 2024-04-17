use ssh_rs::ssh;

fn main() {
    let home_var = std::env::var("HOME").unwrap();

    ssh::enable_log();

    let mut session = ssh::create_session()
        .username("besmith")
        .private_key_path(format!("{}/.ssh/id_rsa", home_var))
        .connect("10.0.1.2:22")
        .unwrap()
        .run_local();
    let exec = session.open_exec().unwrap();
    let vec: Vec<u8> = exec.send_command("ls -al").unwrap();
    println!("{}", String::from_utf8(vec).unwrap());
    // Close session.
    session.close();
}
