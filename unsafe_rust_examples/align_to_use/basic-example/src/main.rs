#[repr(C, packed)]
#[derive(Debug, Copy, Clone)]
struct MyStruct {
    foo: u16,
    bar: u8,
}

fn main() {
    let v = vec![1u8, 2, 3];

    // I copied this code from Stack Overflow
    // without understanding why this case is safe.
    let (head, body, _tail) = unsafe { v.align_to::<MyStruct>() };
    assert!(head.is_empty(), "Data was not aligned");
    let my_struct = &body[0];

    println!("{:?}", my_struct);
}
