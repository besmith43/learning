use oracle::{Connector};

fn main() -> Result<(), Box<dyn std::error::Error>> {
   
    println!("Starting Program");

    let dbname = std::env::var("DBNAME").expect("database name");
    let dbuser = std::env::var("DBUSER").expect("user name");
    let dbpass = std::env::var("DBPASS").expect("password");
    
    println!("DB Name: {}", dbname);
    
    // Connect to a database.
    let mut connector = Connector::new(dbuser, dbpass, dbname);
    let connector = connector.privilege(oracle::Privilege::Sysdba);

    let conn = connector.connect().unwrap();
    
    // let sql = "select * from guitarInventory.guitars";
    let sql = "select * from guitarInventory.guitars where guitar_name = :1";
    
    // Select a table with a bind variable.
    println!("---------------|---------------|---------------|");
    // let rows = conn.query(sql, &[&30])?;
    // let rows = conn.query(sql, &[])?;
    let rows = conn.query(sql, &[&"Taylor T5z"])?;
    for row_result in rows {
        let row = row_result?;
        // get a column value by position (0-based)
        let guitar_name: String = row.get(0)?;
        // get a column by name (case-insensitive)
        let guitar_description: String = row.get(1)?;
        // Use `Option<...>` to get a nullable column.
        // Otherwise, `Err(Error::NullValue)` is returned
        // for null values.
        // let comm: Option<i32> = row.get(2)?;

        // println!(" {:14}| {:>10}    | {:>10}    |",
                // ename,
                // sal,
                // comm.map_or("".to_string(), |v| v.to_string()));
        println!("{}: {}", guitar_name, guitar_description);
    }
    
    // Another way to fetch rows.
    // The rows iterator returns Result<(String, i32, Option<i32>)>.
    // println!("---------------|---------------|---------------|");
    // let rows = conn.query_as::<(String, i32, Option<i32>)>(sql, &[&10])?;
    // for row_result in rows {
        // let (ename, sal, comm) = row_result?;
        // println!(" {:14}| {:>10}    | {:>10}    |",
                // ename,
                // sal,
                // comm.map_or("".to_string(), |v| v.to_string()));
    // }

// # Ok::<(), oracle::Error>(())
    Ok(())
}
