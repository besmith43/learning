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
		attack(ninja)
	}
}

func attack(target string) {
	fmt.Println("throwing ninja stars at", target)
	time.Sleep(time.Second)
}
