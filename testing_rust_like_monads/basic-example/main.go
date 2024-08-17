package main

import (
	"fmt"

	"github.com/samber/mo"
)

func main() {
	fmt.Println("Hello World!")

	option1 := mo.Some(42)
	// Some(42)
	fmt.Println("Option 1:", option1)

	option1.
		FlatMap(func(value int) mo.Option[int] {
			return mo.Some(value * 2)
		}).
		FlatMap(func(value int) mo.Option[int] {
			return mo.Some(value % 2)
		}).
		FlatMap(func(value int) mo.Option[int] {
			return mo.Some(value + 21)
		}).
		OrElse(1234)
	// 21
	fmt.Println("Option 1:", option1)

	option2 := mo.None[int]()
	// None
	fmt.Println("Option 2:", option2)

	option2.OrElse(1234)
	// 1234
	fmt.Println("Option 2:", option2)

	option3 := option1.Match(
		func(i int) (int, bool) {
			// when value is present
			return i * 2, true
		},
		func() (int, bool) {
			// when value is absent
			return 0, false
		},
	)
	// Some(42)
	fmt.Println("Option 3:", option3)
}
