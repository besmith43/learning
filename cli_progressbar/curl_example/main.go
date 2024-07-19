package main

import (
	"fmt"
	"io"
	"log"
	"net/http"
	"os"
	"strings"

	"github.com/bitfield/script"
	"github.com/fatih/color"
	"github.com/schollz/progressbar/v3"
)

func main() {
	fmt.Println("Hello World")

	req, _ := http.NewRequest("GET", "https://dl.google.com/go/go1.14.2.src.tar.gz", nil)
	resp, _ := http.DefaultClient.Do(req)
	defer resp.Body.Close()

	f, _ := os.OpenFile("go1.14.2.src.tar.gz", os.O_CREATE|os.O_WRONLY, 0644)
	defer f.Close()

	bar := progressbar.DefaultBytes(
		resp.ContentLength,
		"downloading",
	)
	io.Copy(io.MultiWriter(f, bar), resp.Body)

	rawfiles, err := script.ListFiles("*").String()
	if err != nil {
		fmt.Println("Error with ls:", err)
		os.Exit(1)
	}

	files := strings.Split(rawfiles, "\n")

	for _, file := range files {

		if file == "go1.14.2.src.tar.gz" {
			fmt.Println(color.RedString(file))
			continue
		}

		fmt.Println(file)
	}

	e := os.Remove("go1.14.2.src.tar.gz")
	if e != nil {
		log.Fatal(e)
	}
}
