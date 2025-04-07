package main

import "fmt"

func main() {
	fmt.Println("Hello World")

	greet("Bob", "Villa")
}

func greet(fname string, lname string) {

	fmt.Printf("Hello %s %s\n", fname, lname)

	fmt.Printf("Hello %s %s\n",
		fname,
		lname,
	)
}
