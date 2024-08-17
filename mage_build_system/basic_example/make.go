//go:build mage

package main

import (
	"errors"
	"fmt"

	"github.com/magefile/mage/sh"
)

func Run() {
	sh.Run("go", "run", "main.go")
}

func Build() {
	fmt.Println("hello from make.go")
}

func Install() error {
	return errors.New("boo!")
}
