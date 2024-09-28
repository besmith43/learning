package main

import (
	"fmt"
	"reflect"
)

func main() {
	args := []any{"thing1", "thing2", 5, true, 25.2}
	printList(args...)
}

func printList(stuffs ...any) {
	for _, item := range stuffs {
		fmt.Println(reflect.TypeOf(item).Kind())
		fmt.Println(item)
	}
}
