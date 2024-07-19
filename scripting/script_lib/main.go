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
}
