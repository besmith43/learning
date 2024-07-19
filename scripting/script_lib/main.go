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
}
