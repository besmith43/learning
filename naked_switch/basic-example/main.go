package main

import (
	"fmt"
	"math/rand"
)

// this came from here:
// https://gist.github.com/savishy/04277beb3eea60a202b792b4f5c54743

func main() {
	x := rand.Intn(200)

	fmt.Printf("x is %d\n", x)

	switch {
	case x > 100:
		fmt.Println("x is very big")
	case x > 10:
		fmt.Println("x is big")
	default:
		fmt.Println("x is small")
	}
}
