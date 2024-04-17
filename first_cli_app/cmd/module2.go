package cmd

import "fmt"

var title string

func Hello() {
	fmt.Println("Hello world from Module 2")
	privHello()
}

func privHello() {
	title := "Module 2"
	fmt.Printf("Hello world from a private function in %s with title variable", title)
	// for some reason this printf has a % at the end of the console string
}
