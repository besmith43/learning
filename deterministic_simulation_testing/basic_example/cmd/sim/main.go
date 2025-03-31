package main

import (
	"fmt"
	"math/rand"
	"time"
	// "github.com/brianvoe/gofakeit"
)

const (
	toggle int = iota
	set
	get
)

func main() {
	fmt.Println("Hello from the Simulation")

	seed := 0
	rounds := 50_000_000
	r := rand.New(rand.NewSource(int64(seed)))
	// gofakeit.Seed(seed)

	bulb := New()

	t := time.Now()

	for _ = range rounds {
		action := r.Int() % 2

		if action == toggle {
			bulb.Toggle()
		} else if action == set {
			err := bulb.SetBrightness(r.Int() % 100)
			if err != nil {
				panic(err)
			}
		} else if action == get {
			br := bulb.GetBrightness()
			if br > 100 || br < 0 {
				panic(fmt.Sprintf("something went wrong\nseed: %d\nbrightness: %d", seed, br))
			}
		} else {
			panic(fmt.Sprintf("an action was called that should be impossible\nseed: %d\n", seed))
		}
	}

	elapsed := time.Since(t)
	fmt.Printf("simulation successfully completed %d actions in %v\n", rounds, elapsed)
}
