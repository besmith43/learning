package main

import (
	"flag"
	"fmt"
	"io"
	"os"

	"github.com/charmbracelet/huh"
)

func main() {

	wordPtr := flag.String("word", "foo", "a string")
	numbPtr := flag.Int("numb", 42, "an int")
	forkPtr := flag.Bool("fork", false, "a bool")

	flag.Parse()

	fmt.Println("word:", *wordPtr)
	fmt.Println("numb:", *numbPtr)
	fmt.Println("fork:", *forkPtr)
	fmt.Println("tail:", flag.Args())

	file := os.Stdin

	fi, err := file.Stat()
	if err != nil {
		fmt.Println("file.stat()", err)
		os.Exit(1)
	}

	if fi.Size() > 0 {
		stdin, err := io.ReadAll(os.Stdin)

		if err != nil {
			panic(err)
		}
		str := string(stdin)

		fmt.Printf("%s", str)
	} else {
		fmt.Println("Stdin is empty")
	}

	var name string

	huh.NewInput().
		Title("What’s your name?").
		Value(&name).
		Run() // this is blocking...

	fmt.Printf("Hey, %s!\n", name)
}
