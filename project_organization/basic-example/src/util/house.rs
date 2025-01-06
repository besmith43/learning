
// the struct also has to be public
// and for the println macro to be used like this:
// println!("House Struct: {home:?}");
// you have to derive the debug trait
#[derive(Debug)]
pub struct House {
    // for these fields to be visable by main.rs
    // you have to make them public
    pub owner: String,
    pub address: String,
}
