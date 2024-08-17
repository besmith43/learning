package main

import "fmt"

// taking this blog post as a source for this example
// https://www.dolthub.com/blog/2024-07-12-golang-range-iters-demystified/

// function for new thing
func iter0(yield func() bool) {
	for range 3 {
		if !yield() {
			return
		}
	}
}

func main() {
	fmt.Println("Hello World!")

	// bug fix as it used to stop early or something
	for i := range 10 {
		fmt.Printf("range 10 iteration %d\n", i)
	}

	// actual new thing
	for range iter0 {
		fmt.Println("iter0")
	}
	// ultimately prints iter0 3 times
}
