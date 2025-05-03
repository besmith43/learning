package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

func main() {

	args := os.Args

	for _, arg := range args {
		fmt.Fprintf(os.Stderr, "%s\n", arg)
	}

	text := read_from_pipe()

	fmt.Println(text)

	fmt.Printf("Hello, stdout!\n")
	fmt.Fprintf(os.Stderr, "Hello, stderr!\n")
}

func read_from_pipe() string {
	var stdin_text strings.Builder

	// check if there is somethinig to read on STDIN
	stat, _ := os.Stdin.Stat()
	if (stat.Mode() & os.ModeCharDevice) == 0 {
		scanner := bufio.NewScanner(os.Stdin)
		for scanner.Scan() {
			stdin_text.WriteString(fmt.Sprintf("%s\n", scanner.Text()))
		}
		if err := scanner.Err(); err != nil {
			log.Fatal(err)
		}
	}

	return stdin_text.String()
}
