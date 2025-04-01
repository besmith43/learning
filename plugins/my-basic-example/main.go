package main

import (
	"errors"
	"fmt"
	"io/fs"
	"log"
	"os"
	"path"
	"plugin"
)

type WebApp interface {
	Response() string
}

func main() {
	fmt.Println("Hello World!")

	run_for_loop()

	run_glob()
}

func run_glob() {
	binPath := "bin"

	plugins := listFiles(binPath)

	fmt.Println("running glob")

	for _, v := range plugins {

		response, err := LoadPlugin(v)
		if err != nil {
			log.Fatal(err)
		}

		fmt.Println(response())

	}
}

func run_for_loop() {

	fmt.Println("running count based for loop")

	for count := range 3 {

		response, err := LoadPlugin(fmt.Sprintf("bin/plugin%d.so", count+1))
		if err != nil {
			log.Fatal(err)
		}

		fmt.Println(response())
	}
}

func LoadPlugin(path string) (func() string, error) {
	plugin, err := plugin.Open(path)
	if err != nil {
		return nil, err
	}

	symWebApp, err := plugin.Lookup("Response")
	if err != nil {
		return nil, err
	}

	webappfunc, ok := symWebApp.(func() string)
	if !ok {
		return nil, errors.New("unexpected type from module symbol")
	}

	return webappfunc, nil
}

func listFiles(dir string) []string {
	root := os.DirFS(dir)

	mdFiles, err := fs.Glob(root, "*.so")

	if err != nil {
		log.Fatal(err)
	}

	var files []string
	for _, v := range mdFiles {
		files = append(files, path.Join(dir, v))
	}
	return files
}
