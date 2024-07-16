package main

import (
	"fmt"
	"io"
	"os"
)

func main() {
	fmt.Println("Hello World")

	// argsWithoutProg := os.Args[1:]

	// fmt.Printf("%s", argsWithoutProg)

	file := os.Stdin

	/*
		the way that this is working is that it's checking os.stdin to see if it has contents
		to do that we are assigning os.Stdin to the variable file and then checking it's Stat()
		if the Size() is greater than zero, it has something and we're using io.ReadAll to read all of the contents in stdin
		else we write to the console that Stdin is empty
	*/

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
}

/*
references

https://stackoverflow.com/questions/22563616/determine-if-stdin-has-data-with-go

https://www.jvt.me/posts/2022/02/21/go-stdin/
*/
