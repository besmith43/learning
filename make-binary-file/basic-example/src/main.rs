use std::io::prelude::*;
use std::io::BufWriter;
use std::fs::File;
use std::mem::size_of;

use anyhow::Result;
use byteorder::BigEndian;
use byteorder::ByteOrder;
use byteorder::{NetworkEndian, NativeEndian, ReadBytesExt, WriteBytesExt};

/*
 * things to remember about variable sizes
 *
 * 1 byte = 8 bits
 * u8's are 1 byte or 0..255
 * 0028 in hex = 40 in decimal
 *
 * u16 = 2 bytes
 * u32 = 4 bytes
 *
 */




const BINARY_FILE: &str = "testfile.bin";
const MAGIC: u32 = 0x4c4c4144;
const VERSION: u16 = 1;
const COUNT: u16 = 1;


struct DbHeader {
    magic: u32,
    version: u16,
    count: u16,
    filesize: u32,
}
// size of = 12 bytes

struct Employee {
    name: String,
    address: String,
    hours: u32, // I hate counting bytes...
}
// size of = 256 + 256 + 8 // 520
// c says that the struct size is 516
// 254 + 254 + 8


fn main() -> Result<()> {

    write()?;

    read()?;

    Ok(())
}

// NOTE: this is a complete overwrite of the file
// also, it's creating the file with a 644
fn write() -> Result<()> {

    let emp = Employee {
        name: "Bob".to_string(),
        address: "123 hamburger ln".to_string(),
        hours: 40,
    };

    // total with 1 employee should be 532 bytes
    // however the c version writes 528 bytes to disk
    // and this rust version is writing 525 bytes to disk
    //
    // don't ask me why, but I'm just making it work
    let temp_filesize = size_of::<DbHeader>() as u32 + (COUNT as u32 * (254 + 254 + 8) /*size_of::<Employee>() as u32*/);
    // let temp_filesize = 525;

    let dbh = DbHeader {
        magic: MAGIC,
        version: VERSION,
        count: COUNT,
        filesize: temp_filesize,
    };

    let mut buffer = BufWriter::new(File::create(BINARY_FILE)?);

    // writing header
    let mut databuf = [0; 4];
    NetworkEndian::write_u32(&mut databuf, dbh.magic);
    buffer.write_all(&databuf)?;

    let mut databuf = [0; 2];
    NetworkEndian::write_u16(&mut databuf, dbh.version);
    buffer.write_all(&databuf)?;

    let mut databuf = [0; 2];
    NetworkEndian::write_u16(&mut databuf, dbh.count);
    buffer.write_all(&databuf)?;

    let mut databuf = [0; 4];
    NetworkEndian::write_u32(&mut databuf, dbh.filesize);
    buffer.write_all(&databuf)?;

    // writing employee
    let mut databuf = [0; 256];
    let letters: Vec<char> = emp.name.chars().collect();

    for x in 0..emp.name.len() {
        databuf[x] = letters[x] as u8;
    }

    buffer.write_all(&databuf)?;

    let mut databuf = [0; 256];
    // let address_length = emp.address.len();
    // let letters = Vec::from(emp.address);
    let letters: Vec<char> = emp.address.chars().collect();

    // for x in 0..address_length {
    for x in 0..emp.address.len() {
        databuf[x] = letters[x] as u8;
    }


    buffer.write_all(&databuf)?;

    let mut databuf = [0; 4];
    NetworkEndian::write_u32(&mut databuf, emp.hours);
    buffer.write_all(&databuf)?;

    buffer.flush()?;
    
    Ok(())
}

// NOTE: reading it back as a u32, we get 1145130060, I probably need to read it in as a string or
// an array of bytes
fn read() -> Result<()> {

    let mut f = File::open(BINARY_FILE)?;

    // MAGIC
    let mut buffer = [0; 4];

    f.read_exact(&mut buffer)?;

    // with NativeEndian the assert is failing with the following message:
    //
    // assertion `left == right` failed
    // left:  1280065860
    // right: 1145130060
    //
    // let raw_data = NativeEndian::read_u32(&buffer);
    let raw_data = BigEndian::read_u32(&buffer); // assert passes with BigEndian..? on a arm64 mac?
                                                 // I guess I should always use BigEndian

    assert_eq!(MAGIC, raw_data);

    let data = raw_data.to_be_bytes();

    println!("Data from binary file: {:?}", data);


    // VERSION
    let mut buffer = [0; 2];

    f.read_exact(&mut buffer)?;

    let version = BigEndian::read_u16(&buffer);

    assert_eq!(VERSION, version);

    println!("Version: {}", version);


    // COUNT
    let mut buffer = [0; 2];

    f.read_exact(&mut buffer)?;

    let count = BigEndian::read_u16(&buffer);

    assert_eq!(COUNT, count);

    println!("COUNT: {}", count);


    // filesize
    let mut buffer = [0; 4];

    f.read_exact(&mut buffer)?;

    let filesize = BigEndian::read_u32(&buffer);

    println!("Filesize: {}", filesize);

    // Employee

    // name
    let mut buffer = [0; 256];

    f.read_exact(&mut buffer)?;

    // println!("{:?}", buffer);

    let emp_name: String = buffer.iter()
        .map(|d| *d as char)
        .collect();

    println!("{}", emp_name);


    // address
    let mut buffer = [0; 256];

    f.read_exact(&mut buffer)?;

    // println!("{:?}", buffer);

    let emp_address: String = buffer.iter()
        .map(|d| *d as char)
        .collect();

    println!("{}", emp_address);


    // hours
    let mut buffer = [0; 4];

    f.read_exact(&mut buffer)?;

    let emp_hours = BigEndian::read_u32(&buffer);

    println!("{}", emp_hours);

    Ok(())
}

