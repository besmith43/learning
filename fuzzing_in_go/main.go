package main

import (
	"errors"
	"fmt"
	"unicode/utf8"
)

func main() {

	input := "The quick brown fox jumped over the lazy dog"

	rev, err := Reverse(input)
	if err != nil {
		panic(err)
	}

	doubleRev, err := Reverse(rev)
	if err != nil {
		panic(err)
	}

	fmt.Printf("original: %q\n", input)
	fmt.Printf("reversed: %q\n", rev)
	fmt.Printf("reversed again: %q\n", doubleRev)

}

func Reverse(s string) (string, error) {

	if !utf8.ValidString(s) {
		return s, errors.New("input is not valid UTF-8")
	}

	r := []rune(s)

	for i, j := 0, len(r)-1; i < len(r)/2; i, j = i+1, j-1 {
		r[i], r[j] = r[j], r[i]
	}

	return string(r), nil
}

/*
// swapping from using bytes to runes for utf8 compatibility
func Reverse(s string) string {

	b := []byte(s)

	for i, j := 0, len(b)-1; i < len(b)/2; i, j = i+1, j-1 {
		b[i], b[j] = b[j], b[i]
	}

	return string(b)
}
*/
