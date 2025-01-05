fn main() {
	use constime::comptime;

	// Let's use a pure-build time dependency
	println!("Here's a fact about the number 5: {}", comptime! {
		extern crate ureq;
		ureq::get("http://numbersapi.com/5/math").call().unwrap().into_string().unwrap()
	});

	// Standard library works fine too.
	println!(
		"Compiled {} seconds after unix epoch.",
		comptime! {
			std::time::SystemTime::now()
				.duration_since(std::time::UNIX_EPOCH)
				.expect("Time went backwards")
				.as_secs()
		}
	);
}
