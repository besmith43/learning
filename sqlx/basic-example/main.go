package main

import (
	"database/sql"
	"fmt"
	"os"

	"github.com/jmoiron/sqlx"
	_ "github.com/mattn/go-sqlite3"
)

func main() {
	fmt.Println("hello world")

	var db *sqlx.DB

	fmt.Println("creating in memory sqlite3 db")
	// exactly the same as the built-in
	db, err := sqlx.Open("sqlite3", ":memory:")
	if err != nil {
		fmt.Printf("error creating db: %s", err)
		os.Exit(1)
	}

	// force a connection and test that it worked
	// _ = db.Ping()

	schema := `CREATE TABLE place (
    country text,
    city text NULL,
    telcode integer);`

	fmt.Println("creating table in db")
	// execute a query on the server
	_, _ = db.Exec(schema)

	fmt.Println("populating table with data")
	// or, you can use MustExec, which panics on error
	cityState := `INSERT INTO place (country, telcode) VALUES (?, ?)`
	countryCity := `INSERT INTO place (country, city, telcode) VALUES (?, ?, ?)`
	db.MustExec(cityState, "Hong Kong", 852)
	db.MustExec(cityState, "Singapore", 65)
	db.MustExec(countryCity, "South Africa", "Johannesburg", 27)

	fmt.Println("running query")
	// fetch all places from the db
	rows, _ := db.Query("SELECT country, city, telcode FROM place")

	fmt.Println("iterating over results")
	// iterate over each row
	for rows.Next() {
		var country string
		// note that city can be NULL, so we use the NullString type
		var city sql.NullString
		var telcode int
		_ = rows.Scan(&country, &city, &telcode)
		fmt.Println(fmt.Sprintf("city: %s country: %s telcode: %d", city, country, telcode))
	}
	// check the error from rows
	// err = rows.Err()

	fmt.Println("running query with where statement")
	row := db.QueryRow("SELECT * FROM place WHERE telcode=?", 852)
	var country string
	var city sql.NullString
	var telcode int
	// err = row.Scan(&telcode)
	err = row.Scan(&country, &city, &telcode)
	fmt.Println(fmt.Sprintf("city: %s country: %s telcode: %d", city, country, telcode))
}
