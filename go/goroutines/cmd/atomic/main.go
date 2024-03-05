package main

import (
	"fmt"
	"sync"
	"sync/atomic"
)

func main() {
	var sum int64
	fmt.Println(sum)
	atomic.AddInt64(&sum, 1)
	fmt.Println(sum)

	// same as a mutex
	var mu sync.Mutex
	mu.Lock()
	sum++
	mu.Unlock()
	fmt.Println(sum)

	var diffSum int64
	fmt.Println(atomic.LoadInt64(&diffSum))
	atomic.StoreInt64(&diffSum, 1)
	fmt.Println(diffSum)

	var av atomic.Value
	wallace := ninja{"Wallace"}
	av.Store(wallace)

	var wg sync.WaitGroup
	wg.Add(1)

	go func() {
		w := av.Load().(ninja)
		w.name = "Not Wallace"
		av.Store(w)
		w.name = "Wallace again"
		wg.Done()
	}()

	wg.Wait()

	fmt.Println(av.Load().(ninja).name)
}

type ninja struct {
	name string
}