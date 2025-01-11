mod sep_file; // this includes the separate file
use sep_file::seperate_func; // can use this so you don't need the :: when you call the function

mod util;
use util::util_hello;
use util::helpers::helper_hello;

// import the house struct
use crate::util::house::House;

fn main() {
    println!("Hello, from main!");
    local_func();
    // sep_file::seperate_func(); // calling the function from the imported mod
    seperate_func(); // can call without naming the module because of the use statement
    util_hello();
    helper_hello();

    let home = House {
        owner: "Me".to_string(),
        address: "123 Hamburger Ln".to_string(),
    };

    println!("House Struct: {home:?}");

    println!("Year Build: {}", House::get_year_built());

    home.print_owner();
}


fn local_func() {
    println!("local function in main.rs");
}
