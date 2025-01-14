#![no_main]

use libfuzzer_sys::fuzz_target;
extern crate bsmath;

fuzz_target!(|data: &[u8]| {
    // fuzzed code goes here
    
    if data.len() == 8 {
        let mut arr = [0u8; 8];
        arr.copy_from_slice(&data);
        let i = u64::from_be_bytes(arr);
        let i = i / 2;

        let _ = bsmath::add(i, i);
        // let _ = BSMath::add(data[0].into(), data[1].into());
    }
});
