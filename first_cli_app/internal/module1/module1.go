package module1

import "fmt"

type Thingy struct {
	Thingy1 string
	Thingy2 string
}

func DoThings() {
	fmt.Println("doing stuff")
}

func DoingThingsWThingy(thingy Thingy) {
	fmt.Println(thingy.Thingy1)
	fmt.Println(thingy.Thingy2)
}
