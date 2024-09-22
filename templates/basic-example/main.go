package main

import (
	"fmt"
	"html/template"
	"os"
	"path"
)

func main() {
	err := os.MkdirAll("example/cmd/program1", 0755)
	if err != nil {
		fmt.Println("failed to make example/cmd/program1")
		fmt.Println(err)
		os.Exit(1)
	}

	var runTmpl = "templates/run.tmpl"

	data, err := os.ReadFile(runTmpl)
	if err != nil {
		fmt.Println("failed to run templates/run.tmpl")
		fmt.Println(err)
		os.Exit(1)
	}

	fmt.Println(string(data))

	// no idea why, but the New param needs to be the basename and not the full path
	// otherwise it'll complain about it being an "incomplete or emtpy" template
	name := path.Base(runTmpl)
	tmpl, err := template.New(name).ParseFiles(runTmpl)
	if err != nil {
		fmt.Println("failed to create and parse template file")
		fmt.Println(err)
		os.Exit(1)
	}

	file, err := os.OpenFile("example/cmd/program1/run.sh", os.O_WRONLY|os.O_CREATE|os.O_APPEND, 0700)
	if err != nil {
		fmt.Println("failed to create and open run.sh file")
		fmt.Println(err)
		os.Exit(1)
	}

	// err = tmpl.Execute(os.Stdout, "program1")
	err = tmpl.Execute(file, "program1")
	if err != nil {
		fmt.Println("failed to execute template")
		fmt.Println(err)
		os.Exit(1)
	}
}
