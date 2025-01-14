#![no_main]

use libfuzzer_sys::fuzz_target;
use arbitrary::Arbitrary;
extern crate bsmath;

#[derive(Arbitrary, Debug)]
pub struct AdditionInput {
    a: u64,
    b: u64,
}

fuzz_target!(|data: AdditionInput| {
    // fuzzed code goes here
    let _ = bsmath::add(data.a/u64::MAX, data.b/u64::MAX);
});
