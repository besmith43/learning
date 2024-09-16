package main

import (
	"fmt"
	"time"
)

func main() {
	for true {
		fmt.Printf(time.Now().Format(time.RFC850))
		fmt.Printf(" - Hello World\n")
		time.Sleep(2 * time.Second)
	}
}
