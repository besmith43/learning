package main

import (
	"fmt"
	"sync"
	"time"
)

var (
	lock   sync.Mutex
	rwlock sync.RWMutex
	count  int
)

func main() {
	// basics()
	readAndWrite()
}

func basics() {
	iterations := 1000
	for i := 0; i < iterations; i++ {
		go increment()
	}

	time.Sleep(time.Second * 5)
	fmt.Println("Resulted count is:", count)
}

func increment() {
	lock.Lock()
	count++
	lock.Unlock()
}

func readAndWrite() {
	go write()
	go read()
	go read()
	go read()
	go read()
	go read()
	go read()
	go read()
	go read()
	go read()
	go write()
	go write()

	time.Sleep(time.Second * 5)
	fmt.Println("Done")
}

func read() {
	rwlock.RLock()
	defer rwlock.RUnlock()

	fmt.Println("Read locking")
	time.Sleep(time.Second * 1)
	fmt.Println("Read unlocking")
}

func write() {
	rwlock.Lock()
	defer rwlock.Unlock()

	fmt.Println("Write locking")
	time.Sleep(time.Second * 1)
	fmt.Println("Write unlocking")
}
