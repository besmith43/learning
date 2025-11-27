package main

import (
	"fmt"

	"github.com/alexflint/go-arg"
)

// https://github.com/alexflint/go-arg

var args struct {
	Input    string   `arg:"positional"`
	Output   []string `arg:"positional"`
	Verbose  bool     `arg:"-v,--verbose" help:"verbosity level"`
	Dataset  string   `help:"dataset to use"`
	Optimize int      `arg:"-O" help:"optimization level"`
}

func main() {
	arg.MustParse(&args)

	fmt.Println("Input:", args.Input)
	fmt.Println("Output:", args.Output)
	fmt.Println("Verbose:", args.Verbose)
	fmt.Println("Dataset:", args.Dataset)
	fmt.Println("Optimize:", args.Optimize)
}
