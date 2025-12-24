package main

import (
	"fmt"
	"io"
	"log"
	"net/http"
	"os"

	"github.com/joho/godotenv"
)

var DOAPIKEY string

func main() {
	err := godotenv.Load()
	if err != nil {
		fmt.Errorf("There was an error loading the dotenv file: %s\n", err.Error())
	}
	DOAPIKEY = os.Getenv("DOAPIKEY")

	mux := http.NewServeMux()
	mux.HandleFunc("GET /", getRoot)
	mux.Handle("GET /v2/droplets", apiMiddleware(http.HandlerFunc(getDroplet)))

	err = http.ListenAndServe(":4000", mux)
	if err != nil {
		log.Fatal(err)
	}
}

func getRoot(w http.ResponseWriter, r *http.Request) {
	fmt.Printf("got / request\n")
	io.WriteString(w, "This is my website!\n")
}
func getDroplet(w http.ResponseWriter, r *http.Request) {
	fmt.Printf("got /v2/droplets request\n")
	io.WriteString(w, "Hello, HTTP from droplet!\n")
}

func apiMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		apiKey := r.Header.Get("Authorization")
		if apiKey != DOAPIKEY {
			http.Error(w, "You are not authorized", http.StatusForbidden)
			return
		}
		next.ServeHTTP(w, r)
	})
}
