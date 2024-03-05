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

	callBack := make(chan bool, 4)
	for _, ninja := range evilNinjas {
		go attack(ninja, callBack)
		fmt.Println(<-callBack)
	}
}

func attack(target string, callBack chan bool) {
	fmt.Println("throwing ninja stars at", target)
	time.Sleep(time.Second)
	callBack <- true
}
