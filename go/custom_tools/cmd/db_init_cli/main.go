package main

import (
	"database/sql"
	"flag"
	"fmt"
	"log"
	"os"

	_ "github.com/microsoft/go-mssqldb"
)

/*

	need a couple of different sub commands

	1) init
		- build out the following folder structure:
			prescripts
			postscripts
			tables
			views
			storedprocedure
	2) reset
		- find and drop all tables
	3) default
		- read and run the sql scripts in the subfolders created by init
	4) test connection
		- take in the parameters and validate that you can connect to the db with the values given
*/

type cmdArgs struct {
	user     string
	password string
	server   string
	port     int64
	catalog  string
}

func main() {

	if len(os.Args) < 2 {
		defaultHelp()
		os.Exit(1)
	}

	switch os.Args[1] {
	case "init":
		initCmd := flag.NewFlagSet("init", flag.ExitOnError)

		initStartingDir := initCmd.String("dir", ".", "starting directory")
		initProjectName := initCmd.String("project", "", "project name")
		initBare := initCmd.Bool("bare", false, "if you don't want example data")

		initCmd.Parse(os.Args[2:])

		if *initProjectName == "" {
			fmt.Println("you have to pass in a project name")
			os.Exit(1)
		}

		_, err := Init(*initStartingDir, *initProjectName, *initBare)
		if err != nil {
			fmt.Println(err.Error())
			os.Exit(1)
		}
	case "reset":
		var options cmdArgs

		resetCmd := flag.NewFlagSet("reset", flag.ExitOnError)

		resetCmd.StringVar(&options.user, "user", "sa", "DB Username")
		resetCmd.StringVar(&options.password, "pw", "Yukon900", "DB User's password")
		resetCmd.StringVar(&options.server, "server", "localhost", "DB Hostname")
		resetCmd.Int64Var(&options.port, "port", 1433, "DB Port")
		resetCmd.StringVar(&options.catalog, "catalog", "msdb", "DB Catalog")

		resetCmd.Parse(os.Args[2:])

		_, err := Reset(options)
		if err != nil {
			fmt.Println(err.Error())
			os.Exit(1)
		}
	case "test":
		var options cmdArgs

		testCmd := flag.NewFlagSet("reset", flag.ExitOnError)

		testCmd.StringVar(&options.user, "user", "sa", "DB Username")
		testCmd.StringVar(&options.password, "pw", "Yukon900", "DB User's password")
		testCmd.StringVar(&options.server, "server", "localhost", "DB Hostname")
		testCmd.Int64Var(&options.port, "port", 1433, "DB Port")
		testCmd.StringVar(&options.catalog, "catalog", "testDB", "DB Catalog")

		testCmd.Parse(os.Args[2:])

		_, err := TestDB(options)
		if err != nil {
			fmt.Println(err.Error())
			os.Exit(1)
		}
		fmt.Println("Successfully connected to server")
	case "run":
		var options cmdArgs

		runCmd := flag.NewFlagSet("run", flag.ExitOnError)

		runCmd.StringVar(&options.user, "user", "sa", "DB Username")
		runCmd.StringVar(&options.password, "pw", "Yukon900", "DB User's password")
		runCmd.StringVar(&options.server, "server", "localhost", "DB Hostname")
		runCmd.Int64Var(&options.port, "port", 1433, "DB Port")
		runCmd.StringVar(&options.catalog, "catalog", "msdb", "DB Catalog")

		runCmd.Parse(os.Args[2:])

		dummy()

	case "version":
		fmt.Println("version: 0.0.1")

	case "help":
		fmt.Println("The following commands are available: version, init, reset, and test")

	default:
		// this will effectively never get called as it will fail on the switch statement
		// as there isn't anything there so it errors out
		defaultHelp()
		os.Exit(1)
	}
}

func dummy() {
	var options cmdArgs

	flag.StringVar(&options.user, "user", "sa", "DB Username")
	flag.StringVar(&options.password, "pw", "Yukon900", "DB User's password")
	flag.StringVar(&options.server, "server", "localhost", "DB Hostname")
	flag.Int64Var(&options.port, "port", 1433, "DB Port")
	flag.StringVar(&options.catalog, "catalog", "msdb", "DB Catalog")

	flag.Parse()

	fmt.Println("user:", options.user)
	fmt.Println("password:", options.password)
	fmt.Println("server:", options.server)
	fmt.Println("port:", options.port)
	fmt.Println("catalog:", options.catalog)

	adoConnectionString := fmt.Sprintf("Data Source=%s;Initial Catalog=%s; User ID=%s; Password=%s; Integrated Security=True;TrustServerCertificate=True;", options.server, options.catalog, options.user, options.password)

	fmt.Println("ms sql server connection string:", adoConnectionString)

	defaultConn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		log.Fatal("Open connection failed:", err.Error())
	}
	defer defaultConn.Close()

	err = ResetDB(defaultConn)
	if err != nil {
		log.Fatal("Reset DB failed:", err.Error())
	}

	_, err = RunGet(defaultConn, "Create Database testDB")
	if err != nil {
		log.Fatal("RunGet Error:", err.Error())
	}

	adoConnectionString = fmt.Sprintf("Data Source=%s;Initial Catalog=testDB; User ID=%s; Password=%s; Integrated Security=True;TrustServerCertificate=True;", options.server, options.user, options.password)

	conn, err := sql.Open("mssql", adoConnectionString)
	if err != nil {
		log.Fatal("Open connection failed:", err.Error())
	}
	defer conn.Close()

	_, err = RunGet(conn, "Create table Widget (WidgetID int, WidgetName varchar(50))")
	if err != nil {
		log.Fatal("RunGet Error:", err.Error())
	}
}

func ResetDB(conn *sql.DB) error {
	// rows, err := RunGet(conn, "select name from SYSOBJECTS where xtype = 'U'")
	// if err != nil {
	// 	return err
	// }

	// for rows.Next() {
	// 	var tableName string
	// 	err = rows.Scan(&tableName)
	// 	if err != nil {
	// 		log.Fatal("Row range error:", err.Error())
	// 	}

	// 	_, err = RunGet(conn, fmt.Sprintf("Drop table %s", tableName))
	// 	if err != nil {
	// 		log.Fatal("drop table error:", err.Error())
	// 	}
	// }

	_, err := RunGet(conn, "Drop database if exists testDB")
	if err != nil {
		return err
	}

	return nil
}

func RunGet(conn *sql.DB, query string) (*sql.Rows, error) {
	stmt, err := conn.Prepare(query)
	if err != nil {
		// log.Fatal("Prepare failed:", err.Error())
		return nil, err
	}
	defer stmt.Close()
	// fmt.Println("Running Query")

	rows, err := stmt.Query()
	if err != nil {
		// log.Fatal("Query failed:", err.Error())
		return nil, err
	}

	return rows, nil
}

func defaultHelp() {
	fmt.Println("You didn't give a command")
	fmt.Println("the available ones are: init, reset, and test")
}
