package main

import "embed"

//go:embed storage/gopher.png
var fileString string

//go:embed storage/gopher.png
var fileByte []byte

//go:embed storage/gopher.png
//go:embed storage/test.pdf
var folder embed.FS

func GetEmbeddedGopher() (string, error) {
	// print(fileString)
	// print(string(fileByte))

	content1, err := folder.ReadFile("storage/test.pdf")
	print(string(content1))

	return string(content1), err
}
