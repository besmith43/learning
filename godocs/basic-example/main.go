package main

import (
	"example/area"
	"fmt"
)

// Main function
func main() {
	fmt.Println("Hello World!")

	thingy := area.Square{Side: 10}

	fmt.Printf("The Square's Area is %f\n", thingy.Area())
}
