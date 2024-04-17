package main

import (
	"database/sql"
	"fmt"
)

type thingy struct {
	WidgetID   int
	WidgetName sql.NullString
}

func TestDB(dbConnection cmdArgs) (bool, error) {
	fmt.Println("Test CMD DB Server name:", dbConnection.server)

	adoConnectionString := fmt.Sprintf("Data Source=%s;Initial Catalog=testDB; User ID=%s; Password=%s; Integrated Security=True;TrustServerCertificate=True;", dbConnection.server, dbConnection.user, dbConnection.password)

	conn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		fmt.Println("connecting to db failed.  exiting testdb early")
		fmt.Println(err.Error())
		return false, err
	}
	defer conn.Close()

	// var t thingy

	_, err = conn.Query("select 1")
	if err != nil {
		return false, err
	} else {
		return true, nil
	}
	// row := conn.QueryRow("select * from Widget where WidgetID = ?", 4)
	// err = row.Scan(&t.WidgetID, &t.WidgetName)
	// if err != nil {
	// 	fmt.Println("scan failed.  exiting testdb early")
	// 	fmt.Println(err.Error())
	// 	return false, err
	// }

	// if t.WidgetName.Valid {
	// 	fmt.Printf("Query Results: ID = %d\nName = %s\n", t.WidgetID, t.WidgetName.String)
	// } else {
	// 	fmt.Printf("Query Results: ID = %d\nName = null\n", t.WidgetID)
	// }

	// return true, nil
}
