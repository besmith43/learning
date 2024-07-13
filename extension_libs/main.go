package main

import (
	"fmt"

	"github.com/samber/lo"
	"github.com/samber/mo"
)

func main() {
	fmt.Println("Hello World")

	// https://github.com/samber/lo
	names := lo.Uniq([]string{"Samuel", "John", "Samuel"})
	// []string{"Samuel", "John"}

	fmt.Println("Unique Names: %s", names)

	option1 := mo.Some(42)
	// Some(42)
	fmt.Println("Some: %d", option1)
	/*
		option1.
			FlatMap(func(value int) Option[int] {
				return Some(value * 2)
			}).
			FlatMap(func(value int) Option[int] {
				return Some(value % 2)
			}).
			FlatMap(func(value int) Option[int] {
				return Some(value + 21)
			}).
			OrElse(1234)
		// 21

		option2 := mo.None[int]()
		// None

		option2.OrElse(1234)
		// 1234

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
	*/
}
