package main

import (
	"log"
	"net/http"
)

func main() {
	mux := http.NewServeMux()

	mux.Handle("GET /", messageHandler("Hello world!"))

	log.Print("listening on :3000...")
	err := http.ListenAndServe(":3000", mux)
	log.Fatal(err)
}

func messageHandler(message string) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte(message))
	})
}
