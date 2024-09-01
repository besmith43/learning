package main

import (
	"fmt"
	"os"

	"github.com/bitfield/script"
)

// see link for article about the library: https://bitfieldconsulting.com/posts/scripting

func main() {
	fmt.Println("Hello World")

	contents, err := script.File("test.txt").String()
	if err != nil {
		fmt.Println("Error reading the test.txt file with error message:", err)
		os.Exit(1)
	}

	fmt.Println("test.txt Contents:", contents)

	script.Get("https://wttr.in/London?format=3").Stdout()

	// you need the trailing \n because echo isn't doing a new line on it's own
	script.Echo("data\n").Tee().AppendFile("data.txt")

	numLines, err := script.File("data.txt").CountLines()
	if err != nil {
		fmt.Println("Error with data.txt:", err)
		os.Exit(1)
	}

	fmt.Printf("Data.txt has %d lines\n", numLines)

	err = os.Mkdir("testdir", 0755)
	if err != nil {
		fmt.Println("Error making testdir:", err)
		os.Exit(1)
	}

	err = os.Mkdir("testdir2", 0755)
	if err != nil {
		fmt.Println("Error making testdir2:", err)
		os.Exit(1)
	}

	var testfilecontents = "this is a test of the emergency broadcast system"

	_, err = script.Echo(testfilecontents).WriteFile("./testdir/testfile.txt")
	if err != nil {
		fmt.Println("Error write text file to disc:", err)
		os.Exit(1)
	}

	err = os.Rename("./testdir/testfile.txt", "./testdir2/testfile.txt")
	if err != nil {
		fmt.Println("Error while moving/renaming testfile from testdir to testdir2:", err)
		os.Exit(1)
	}

	script.ListFiles("./testdir").Stdout()
	script.ListFiles("./testdir2").Stdout()

	err = os.RemoveAll("./testdir")

	if err != nil {
		fmt.Println("Error deleting testdir:", err)
		os.Exit(1)
	}

	err = os.RemoveAll("./testdir2")

	if err != nil {
		fmt.Println("Error deleting testdir2:", err)
		os.Exit(1)
	}

	script.ListFiles(".").Stdout()

	_, err = script.ListFiles("../../").Stdout()
	if err != nil {
		fmt.Println("Error list home dir files:", err)
		os.Exit(1)
	}

	dirs, err := os.ReadDir("../../")
	if err != nil {
		fmt.Println("Error list home dir files:", err)
		os.Exit(1)
	}

	for _, dir := range dirs {
		fmt.Println(dir.Name())
	}
}
