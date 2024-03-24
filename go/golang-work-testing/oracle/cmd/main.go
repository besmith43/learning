package main

import (
	"database/sql"
	"fmt"
	"log"

	_ "github.com/microsoft/go-mssqldb"
	_ "github.com/sijms/go-ora/v2"
)

/*
	see here for how Oracle says to connect to their database
	https://developer.oracle.com/learn/technical-articles/way-to-go-on-oci-article4
*/

type bob struct {
	smith string
}

func main() {
	fmt.Println("Hello, World!")

	stuff := bob{smith: "stuff & "}

	fmt.Println(stuff.Thingy())

	bannerConnectionString, err := GetBannerConnectionString()
	if err != nil {
		log.Fatal("GetBannerConnectionString failed:", err.Error())
	}

	fmt.Println("Banner Connection String: ", bannerConnectionString)

	db, err := sql.Open("oracle", bannerConnectionString)
	if err != nil {
		log.Fatal("Open connection failed:", err.Error())
	}

	defer db.Close()

	strQuery := "SELECT ID, FNAME, LNAME FROM TTU_CURRENTLY_EMPLOYED WHERE ID = :ID"

	rows, err := db.Query(strQuery, sql.Named("ID", "T00099833"))
	if err != nil {
		log.Fatal("Query failed:", err.Error())
	}

	for rows.Next() {
		var id string
		var fname string
		var lname string
		err := rows.Scan(&id, &fname, &lname)
		if err != nil {
			log.Fatal("Scan failed:", err.Error())
		}
		fmt.Println(id, fname, lname)
	}
}

func (s *bob) Thingy() string {
	return fmt.Sprintf("%s thingy", s.smith)
}

func GetBannerConnectionString() (string, error) {
	adoConnectionString := "Data Source=SQLCLS03;Initial Catalog=PortalProd; Integrated Security=True;TrustServerCertificate=True;"
	strQuery := fmt.Sprintf("SELECT BannerHost, BannerPort, BannerServiceName, BannerUsername, BannerPassword FROM express.tblLocalizations WHERE Instance = '%s'", "Production")

	conn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		return "Open connection failed:", err
	}
	defer conn.Close()

	stmt, err := conn.Prepare(strQuery)
	if err != nil {
		return "Prepare failed", err
	}
	defer stmt.Close()

	rows, err := stmt.Query()
	if err != nil {
		return "Query failed", err
	}

	// rows, err := conn.QueryContext(ctx, strQuery, "'StreamLynePersonUpload'")
	// if err != nil {
	// 	log.Fatal("Query failed:", err.Error())
	// }

	for rows.Next() {
		var BannerHost string
		var BannerPort string
		var BannerServiceName string
		var BannerUsername string
		var BannerPassword string
		err := rows.Scan(&BannerHost, &BannerPort, &BannerServiceName, &BannerUsername, &BannerPassword)
		if err != nil {
			log.Fatal("Scan failed:", err.Error())
		}

		// example of how to generate a connection string
		// connectionString := "oracle://" + dbParams["username"] + ":" + dbParams["password"] + "@" + dbParams["server"] + ":" + dbParams["port"] + "/" + dbParams["service"]
		return fmt.Sprintf("oracle://%s:%s@%s:%s/%s", BannerUsername, BannerPassword, BannerHost, BannerPort, BannerServiceName), nil
		// return fmt.Sprintf("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=%s)(PORT=%s))(CONNECT_DATA=(Server=DEDICATED)(SERVICE_NAME = %s))); User Id = %s; Password = %s;", BannerHost, BannerPort, BannerServiceName, BannerUsername, BannerPassword), nil
	}

	// Check for errors from iterating over rows.
	if err := rows.Err(); err != nil {
		return "Row Errors", err
	}

	return "No rows found", nil
}
