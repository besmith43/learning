package main

import (
	"database/sql"
	"fmt"

	_ "github.com/ncruces/go-sqlite3/driver"
	_ "github.com/ncruces/go-sqlite3/embed"
)

func main() {
	fmt.Println("hello world")

	var version string
	db, _ := sql.Open("sqlite3", "file:demo.db")
	db.QueryRow(`SELECT sqlite_version()`).Scan(&version)

	fmt.Println("sqlite3 version: ", version)
}
