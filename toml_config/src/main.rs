use toml::*;
use chrono::prelude::*;
use fs_extra::file::{write_all, read_to_string};
use serde_derive::{Deserialize, Serialize};
use std::path::Path;
use std::fs::create_dir_all;

#[derive(Deserialize, Serialize)]
struct Config {
    log_name: String,
}

fn main() {
    dir_setup();


    let config_location = String::from("./config/example.toml");

    let mut config: Config;

    if Path::new(&config_location).is_file() {
        let config_string = read_to_string(&config_location).unwrap();

        config = toml::from_str(&config_string).unwrap();
    } else {
        config = Config { log_name: "./tmp/toml_config_log.txt".to_string() };
        generate_basic_toml(&config_location, &config);
    }

    log_message(&config.log_name, &"new log message");
}

fn dir_setup() {
    create_dir_all("./config").unwrap();
    create_dir_all("./tmp").unwrap();
}


fn generate_basic_toml(filename: &str, conf: &Config) {
    let toml = toml::to_string(&conf).unwrap();
    write_all(&filename, &toml).unwrap();
}

fn log_message(filename: &str, message: &str) {
    let mut tmp_content;

    if Path::new(&filename).is_file() {
        tmp_content = read_to_string(&filename).unwrap();
    } else {
        tmp_content = String::new();
    }

    let temp = format!("{} - {}\n", Utc::now(), &message);

    tmp_content.push_str(&temp);
    write_all(&filename, &tmp_content).unwrap();
}
