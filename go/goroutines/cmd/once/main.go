package main

import (
	"fmt"
	"math/rand"
	"sync"
)

var missionCompleted bool

func main() {
	var wg sync.WaitGroup
	wg.Add(100)

	var once sync.Once

	for i := 0; i < 100; i++ {
		go func() {
			if foundTreasure() {
				once.Do(markMissionCompleted)
			}
			wg.Done()
		}()
	}

	wg.Wait()

	checkMissionsCompletion()
}

func checkMissionsCompletion() {
	if missionCompleted {
		fmt.Println("Mission is now completed")
	} else {
		fmt.Println("Mission was a failure")
	}
}

func markMissionCompleted() {
	missionCompleted = true
	fmt.Println("Marking Misssion as completed")
}

func foundTreasure() bool {
	return 0 == rand.Intn(10)
}
