package main

import (
	"database/sql"
	"fmt"
	"log"

	_ "github.com/microsoft/go-mssqldb"
)

// var ctx context.Context

func main() {
	fmt.Println("Hello, World!")

	RunQuery()
}

func RunQuery() {
	adoConnectionString := "Data Source=SQLCLS03;Initial Catalog=ServicesSafariProd; Integrated Security=True;TrustServerCertificate=True;"
	strQuery := fmt.Sprintf("SELECT ServiceName, Description, Status FROM [ss].[tblServices] where ServiceName = '%s'", "StreamLynePersonUpload")
	// strQuery := "SELECT ServiceName, Description, Status FROM [ss].[tblServices] where ServiceName = @ServiceName"

	conn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		log.Fatal("Open connection failed:", err.Error())
	}
	defer conn.Close()

	stmt, err := conn.Prepare(strQuery)
	if err != nil {
		log.Fatal("Prepare failed:", err.Error())
	}
	defer stmt.Close()
	fmt.Println("Running Query")

	rows, err := stmt.Query()
	if err != nil {
		log.Fatal("Query failed:", err.Error())
	}

	// rows, err := conn.QueryContext(ctx, strQuery, "'StreamLynePersonUpload'")
	// if err != nil {
	// 	log.Fatal("Query failed:", err.Error())
	// }

	for rows.Next() {
		var ServiceName string
		var Description string
		var Status string
		err := rows.Scan(&ServiceName, &Description, &Status)
		if err != nil {
			log.Fatal("Scan failed:", err.Error())
		}
		fmt.Println(ServiceName, Description, Status)
	}

	// Check for errors from iterating over rows.
	if err := rows.Err(); err != nil {
		log.Fatal(err)
	}
}
