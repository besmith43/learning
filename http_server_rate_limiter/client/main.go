package main

import (
	"fmt"
	"log"
	"net/http"
)

func main() {
	for i := range 10 {
		fmt.Printf("%d. ", i)
		resp, err := http.Get("http://localhost:4000")
		if err != nil {
			log.Fatal(err)
		}
		defer resp.Body.Close()

		fmt.Printf("http status code: %d %s\n", resp.StatusCode, http.StatusText(resp.StatusCode))
	}
}
