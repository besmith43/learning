use sibyl as oracle; // pun intended :)

// #[cfg(feature="blocking")]
fn main() -> Result<(),Box<dyn std::error::Error>> {
    let oracle = oracle::env()?;

    let dbname = std::env::var("DBNAME").expect("database name");
    let dbuser = std::env::var("DBUSER").expect("user name");
    let dbpass = std::env::var("DBPASS").expect("password");

    let session = oracle.connect(&dbname, &dbuser, &dbpass)?;
    let stmt = session.prepare("
        select * from guitarInventory.guitars
    ")?;
    // let date = oracle::Date::from_string("January 1, 2005", "MONTH DD, YYYY", &session)?;
    let rows = stmt.query("")?;
    while let Some( row ) = rows.next()? {
        let guitar_name : &str          = row.get(0)?;
        let guitar_description : &str   = row.get(1)?;

        println!("{}: {}", guitar_name, guitar_description);
    }
    if stmt.row_count()? == 0 {
        // println!("No one was hired after {}", date.to_string("FMMonth DD, YYYY")?);
        println!("There is not guitar inventory");
    }
    Ok(())
}
