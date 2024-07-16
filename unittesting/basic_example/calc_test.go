package main

import (
	"testing"
)

func TestAdd(t *testing.T) {
	ans := Add(5, 3)
	if ans != 8 {
		t.Errorf("Add(5, 3) gave %d, but expected 8", ans)
	}
}
