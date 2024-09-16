package main

import (
	"fmt"
	"time"
)

func main() {
	fmt.Printf(time.Now().Format(time.RFC850))
	fmt.Printf(" - starting service\n")

	for true {
		fmt.Printf(time.Now().Format(time.RFC850))
		fmt.Printf(" - Hello World\n")
		time.Sleep(5 * time.Second)
	}

	fmt.Printf(time.Now().Format(time.RFC850))
	fmt.Printf(" - stopping service\n")
}
