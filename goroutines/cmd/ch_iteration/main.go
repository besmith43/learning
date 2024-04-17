package main

import (
	"fmt"
	"math/rand"
)

/*
	what's happening here is that the channel has a capacity of 0
	so it can only hold a single message at a time
	so the main goroutine is awaiting on the channel to get a message
	then run the print line immediately after there's a message in the channel

	tldr: this is running like the mental model of async/await
*/

func main() {
	fmt.Println("Running the first practice")
	main2()

	fmt.Println("Running the second practice")
	main3()

	fmt.Println("Done with practice for today")
}

func main2() {
	callBack := make(chan string, 3)
	numRounds := 3
	go throwingNinjaStar(callBack, numRounds)

	for i := 0; i < numRounds; i++ {
		fmt.Println(<-callBack)
	}
}

func throwingNinjaStar(callBack chan string, numRounds int) {
	for i := 0; i < numRounds; i++ {
		score := rand.Intn(10)
		callBack <- fmt.Sprint("You scored: ", score)
	}
}

/*
	this version is running more as a while look as the main loop has no idea how many iterations/messages to expect from the channel
*/

func main3() {
	callBack := make(chan string)
	go throwingNinjaStarV3(callBack)
	for message := range callBack {
		fmt.Println(message)
	}
}

/*
	this is what's happening under the hood in the ranged foreach loop in main3
*/

func main4() {
	callBack := make(chan string)
	go throwingNinjaStarV3(callBack)
	for {
		message, open := <-callBack
		if !open {
			break
		}
		fmt.Println(message)
	}
}

func throwingNinjaStarV3(callBack chan string) {
	numRounds := 3
	for i := 0; i < numRounds; i++ {
		score := rand.Intn(10)
		callBack <- fmt.Sprint("You scored: ", score)
	}

	// this line fixes the deadlock that happens as it closes the channel and that signals to the main function that the goroutine is done running
	close(callBack)
}
