package main

import (
	"fmt"
	"testing"
)

func main() {
	fmt.Println("Hello world!")
	ans := Add(5, 8)
	fmt.Printf("Adding 5 & 8 gave %d\n", ans)
}

func Add(a int, b int) int {
	return a + b
}

func TestAdd(t *testing.T) {
	ans := Add(5, 3)
	if ans != 8 {
		t.Errorf("Add(5, 3) gave %d, but expected 8", ans)
	}
}
