package main

import (
	"fmt"
	"os"

	"golang.org/x/term"
)

func main() {
	width, height, err := term.GetSize(int(os.Stdout.Fd()))
	if err != nil {
		fmt.Println("Error getting console size:", err)
		return
	}
	fmt.Println("Console height:", height)
	fmt.Println("Console width:", width)
}
