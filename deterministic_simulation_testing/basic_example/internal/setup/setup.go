package setup

import (
	"database/sql"
	"fmt"

	_ "github.com/ncruces/go-sqlite3/driver"
	_ "github.com/ncruces/go-sqlite3/embed"
)

func db_setup(db_path string) *sql.DB {
	db, _ := sql.Open("sqlite3", fmt.Sprintf("file:%s", db_path))

	db.Exec(`CREATE TABLE IF NOT EXISTS tablename (id INTEGER PRIMARY KEY AUTOINCREMENT, store TEXT, amount NUMERIC, date INTEGER)`)

	return db
}
