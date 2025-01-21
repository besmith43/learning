pub fn get_data() -> String {
    "This is a test of the emergency broadcast system".to_string()
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        insta::assert_snapshot!(get_data());
    }
}
