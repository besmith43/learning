package main

import (
	"database/sql"
	"fmt"
)

func Reset(dbConnection cmdArgs) (bool, error) {
	fmt.Println("Reset CMD DB Name:", dbConnection.server)

	adoConnectionString := fmt.Sprintf("Data Source=%s;Initial Catalog=testDB; User ID=%s; Password=%s; Integrated Security=True;TrustServerCertificate=True;", dbConnection.server, dbConnection.user, dbConnection.password)

	conn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		fmt.Println("connecting to db failed.  exiting testdb early")
		fmt.Println(err.Error())
		return false, err
	}
	defer conn.Close()

	getTables := "SELECT name FROM SYSOBJECTS WHERE xtype = 'U'"

	rows, err := conn.Query(getTables)
	for rows.Next() {
		var tableName string
		err = rows.Scan(&tableName)
		if err != nil {
			return false, err
		}

		_, err = conn.Query(fmt.Sprintf("Drop table %s", tableName))
		if err != nil {
			return false, err
		}
	}

	return true, nil
}
