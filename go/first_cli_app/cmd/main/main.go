package main

import (
	"FirstCliApp/cmd"
	"FirstCliApp/internal/module1"
	"bufio"
	"fmt"
	"os"
)

var (
	internalString string
)

/*
	see this article for nongithub based module import... I'm not a fan at the moment...

	https://yuminlee2.medium.com/golang-import-local-packages-cbb2201c72e1
*/

/*
	to setup debug follow this guide
	https://blog.logrocket.com/debugging-go-vs-code/

	and install dlv from https://github.com/go-delve/delve/tree/master/Documentation/installation
*/

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	fmt.Println("Enter some text (press Ctrl+D or Ctrl+Z to end):")

	for scanner.Scan() {
		text := scanner.Text()
		if text == "" {
			break
		}
		fmt.Println("You entered:", text)
	}

	if err := scanner.Err(); err != nil {
		fmt.Println("Error:", err)
	}

	fmt.Println("Hello world")

	t := module1.Thingy{Thingy1: "testing", Thingy2: "is this thing on?"}

	module1.DoThings()

	module1.DoingThingsWThingy(t)

	cmd.Hello()

	// this doesn't work because it's a private function as indicated by the lower case letter to start the function name
	// cmd.privHello()
}
