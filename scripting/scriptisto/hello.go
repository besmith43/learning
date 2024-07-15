#!/usr/bin/env scriptisto

// scriptisto-begin
// script_src: hello.go
// build_cmd: go build -o hello ./hello.go
// scriptisto-end

package main

import (
	"fmt"
	//"os"
)

func main() {
	fmt.Print("Enter your name: ")
	var name string
	fmt.Scanln(&name)

	fmt.Printf("Hello, %s! Scripting in Go is powerful.\n", name)
}
