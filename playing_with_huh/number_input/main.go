package main

import (
	"fmt"
	"os"
	"strconv"

	"github.com/charmbracelet/huh"
)

func main() {
	var name string

	huh.NewInput().
		Title("What’s your name?").
		Value(&name).
		Run() // this is blocking...

	fmt.Printf("Hey, %s!\n", name)

	var lonelyNumber string

	huh.NewInput().
		Title("What’s the loneliest number?").
		Value(&lonelyNumber).
		Run() // this is blocking...

	i, err := strconv.Atoi(lonelyNumber)
	if err != nil {
		fmt.Printf("%s is not a number\n", lonelyNumber)
		os.Exit(1)
	}

	fmt.Printf("Correct, %d is the loneliest number!\n", i)

}
