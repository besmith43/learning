package main

import (
	"fmt"
	"sync"
)

func main() {
	// main_single()
	main_multiple()
	// main_common_errors()
}

func main_single() {
	var beeper sync.WaitGroup

	beeper.Add(1)

	go attack("Tommy", &beeper)

	beeper.Wait()

	fmt.Println("Mission completed")
	// this will only sometimes show the attack print statement
	// without the waitgroup
}

func main_multiple() {
	var beeper sync.WaitGroup

	evilNinjas := []string{"Tommy", "Johnny", "Bobby"}

	beeper.Add(len(evilNinjas))

	// index, string
	for _, evilNinja := range evilNinjas {
		go attack(evilNinja, &beeper)
	}

	beeper.Wait()

	fmt.Println("Mission completed")

}

func main_common_errors() {
	infinite_deadlock()
	too_many_dones()
}

func infinite_deadlock() {
	var beeper sync.WaitGroup
	beeper.Add(1)
	beeper.Wait()
}

func too_many_dones() {
	var beeper sync.WaitGroup
	beeper.Add(1)
	beeper.Done()
	beeper.Done()
	beeper.Wait()
}

func attack(evilNinja string, beeper *sync.WaitGroup) {
	fmt.Println("Attacked evil ninja: ", evilNinja)
	beeper.Done()
}
