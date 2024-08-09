//go:build !DEBUG

package main

import "fmt"

func myLog(message string) {
	fmt.Printf("PROD: %s\n", message)
}
