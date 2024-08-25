package main

import (
	"errors"
	"fmt"
	"log"
	"plugin"
)

type WebApp interface {
	Response() string
}

func main() {
	fmt.Println("Hello World!")

	response, err := LoadPlugin()
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println(response())
}

func LoadPlugin() (func(), error) {
	plugin, err := plugin.Open("bin/plugin1.so")
	if err != nil {
		return nil, err
	}

	symWebApp, err := plugin.Lookup("Response")
	if err != nil {
		return nil, err
	}

	webappfunc, ok := symWebApp.(func())
	if !ok {
		return nil, errors.New("unexpected type from module symbol")
	}

	return webappfunc, nil
}
