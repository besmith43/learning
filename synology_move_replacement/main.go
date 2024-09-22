package main

import (
	"fmt"
	"os"
	"os/exec"
	"syscall"
)

func main() {

	src := os.Args[1]
	dest := os.Args[2]

	fmt.Printf("moving from %s to %s\n", src, dest)

	err := os.Rename(src, fmt.Sprintf("%s/%s", dest, src))
	if err != nil {
		fmt.Println(err)

		fmt.Println("trying to call the system mv")
		err = sys_move(src, dest)
		if err != nil {
			fmt.Println(err)
			os.Exit(1)
		}
	}
}

func sys_move(src string, dest string) error {
	binary, err := exec.LookPath("mv")
	if err != nil {
		fmt.Println(err)
		return err
	}

	args := []string{"mv", src, dest}

	env := os.Environ()

	err = syscall.Exec(binary, args, env)
	if err != nil {
		fmt.Println(err)
		return err
	}

	return nil
}
