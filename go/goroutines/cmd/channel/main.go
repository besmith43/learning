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
		callBack := make(chan bool)
		go attack(ninja, callBack)
		fmt.Println(<-callBack)
	}
}

func attack(target string, callBack chan bool) {
	fmt.Println("throwing ninja stars at", target)
	time.Sleep(time.Second)
	callBack <- true
}
