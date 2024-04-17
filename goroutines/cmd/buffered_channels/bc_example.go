package main

import "fmt"

/*
	if you comment out one of the "buffed <-" lines then you'll get a deadlock because the channel is expecting a second result and hangs waiting
*/

func bc_example() {
	buffed := make(chan string, 2)
	buffed <- "First Channel"
	buffed <- "Second Channel"
	fmt.Println(<-buffed)
	fmt.Println(<-buffed)
}
