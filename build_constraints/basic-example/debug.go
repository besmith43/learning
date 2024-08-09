//go:build DEBUG

package main

import "fmt"

func myLog(message string) {
	fmt.Printf("DEBUG: %s\n", message)
}
