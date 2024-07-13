package main

import (
	"fmt"

	trie "github.com/Vivino/go-autocomplete-trie"
)

func main() {
	t := trie.New()
	t.Insert("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")

	fmt.Println(t.SearchAll("wdn"))
}
