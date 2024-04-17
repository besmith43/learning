use xshell::{Shell, cmd};
use std::process;
use anyhow::Result;

fn main() {
    if let Err(err) = try_main() {
        eprintln!("{}", err);
        process::exit(1);
    }
}

fn try_main() -> Result<()> {
    let sh = Shell::new()?;
    let branch = "master";
    let commit_hash = cmd!(sh, "git rev-parse {branch}").run();
    Ok(())
}
