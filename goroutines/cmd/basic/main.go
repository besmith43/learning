package main

import (
	"fmt"
	"time"
)

func main() {
	start := time.Now()
	defer func() {
		fmt.Println(time.Since(start))
	}()

	evilNinjas := []string{"Tommy", "Johnny", "Bobby", "Andy"}

	for _, ninja := range evilNinjas {
		// adding "go" is the only difference between basic and serial
		// this change causes the main func to fire and forget the 4 goroutines to process a unique various of the attack func
		// this is way the deferred func will basically show 0 seconds taken to run
		// tldr: main isn't waiting on the attack functions to complete
		go attack(ninja)
	}
}

func attack(target string) {
	fmt.Println("throwing ninja stars at", target)
	time.Sleep(time.Second)
}
