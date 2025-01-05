
// light switch example
#[warn(dead_code)] // required to make a dead code warning go away
struct Light<State> {
	state: State,
}
struct On;
struct Off;


impl Light<Off> {
	fn new()         -> Self      { Light { state: Off} }
	fn turn_on(self) -> Light<On> { Light { state: On } }
}
impl Light<On> {
	fn turn_off(self) -> Light<Off> {
		Light { state: Off }
	}
}


fn correct_transitions() {

	let bedroom_light = Light::new();
	let _bedroom_light = bedroom_light // _ is also required as the new "shadowed" version of
                                       // bedroom_light is never used
		.turn_on()
		.turn_off()
		.turn_on();
	
}

// or implement like this

impl Light<Off> {
  fn flip(self) -> Light<On> { Light { state: On } }
}
impl Light<On> {
  fn flip(self) -> Light<Off> { Light { state: Off } }
}
/*impl<State> Light<State> {
  fn example(self) -> i32 { todo!("read the post") }
}*/

fn test_it() {
	let bedroom_light = Light { state: Off };
	bedroom_light.flip().flip().flip(); //easy does it
}

fn main() {
    correct_transitions();
    test_it();
}








// you can ignore this

// identification example
/*
enum ID {
    V4(u8, u8, u8, u8),
    V6(u16, u16, u16, u16, u16, u16, u16, u16),
    Mac(u8, u8, u8, u8, u8, u8),
	// New variants:
	FreqHz(u64),
	Coord { lat: f64, lon: f64 },
	Uuid([u8; 16]),
}

fn send_packet(node: &ID) {
  match node {
    ID::V4(..) | ID::V6(..) => ip_packet(node),
    ID::Mac(..)             => ethernet_frame(node),
    // because of the new variants, we have to add the following or the compiler won't run our code
    ID::FreqHz(..)          => aprs_broadcast(node),
    ID::Coord{..}           => geocach(node),
    ID::Uuid(..)            => store(node)
  }
}
*/




