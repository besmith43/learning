use std::io::prelude::*;
use std::net::TcpStream;


fn main() -> std::io::Result<()>{
    let mut stream = TcpStream::connect("localhost:8443")?;

    let get_message = "GET / HTTP/1.1\r\n\r\n";
    let mut response_str: String = Default::default();
    let _write_response = stream.write(&get_message.as_bytes())?;
    let _read_response = stream.read_to_string(&mut response_str).unwrap();

    // dbg!(write_response);
    // dbg!(read_response);
    // dbg!(&response_str);

    let parsed_message = ResponseMessage::new(response_str);

    dbg!(parsed_message);

    Ok(())
}

#[derive(Debug)]
struct ResponseMessage {
    headers: Vec<String>,
    body: Vec<String>,
}

impl ResponseMessage {
    fn new(response: String) -> ResponseMessage {
        let lines = response.lines();

        let mut split = false;

        let mut body: Vec<String> = Vec::new();
        let mut headers: Vec<String> = Vec::new();

        for line in lines {
            if split {
                println!("Body: {:?}", line);
                body.push(line.to_string());
            } else {
                println!("Header: {:?}", line);
                headers.push(line.to_string());
            }

            if line == "" {
                split = true;
            }
        }

        ResponseMessage {
            headers,
            body,
        }
    }
}


