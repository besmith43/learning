package main

import "fmt"

func main() {
	args := []int{1, 2, 3}
	fmt.Println(sum(args...))
}

func sum(nums ...int) int {
	total := 0
	for _, i := range nums {
		total += i
	}

	return total
}
