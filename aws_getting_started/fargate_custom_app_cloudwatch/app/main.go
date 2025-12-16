package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"time"
)

func loggingMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		start := time.Now()

		// Log the request
		log.Printf("Started %s %s from %s", r.Method, r.URL.Path, r.RemoteAddr)

		// Call the next handler
		next.ServeHTTP(w, r)

		// Log the completion
		duration := time.Since(start)
		log.Printf("Completed %s %s in %v", r.Method, r.URL.Path, duration)
	})
}

func helloHandler(w http.ResponseWriter, r *http.Request) {
	hostname, _ := os.Hostname()
	response := fmt.Sprintf("Hello World from %s! 🌍\nRequest received at: %s\n",
		hostname, time.Now().Format(time.RFC3339))

	w.Header().Set("Content-Type", "text/plain")
	w.WriteHeader(http.StatusOK)
	fmt.Fprint(w, response)
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	fmt.Fprint(w, `{"status":"healthy","timestamp":"`+time.Now().Format(time.RFC3339)+`"}`)
}

func main() {
	port := os.Getenv("PORT")
	if port == "" {
		port = "8080"
	}

	mux := http.NewServeMux()
	mux.HandleFunc("/", helloHandler)
	mux.HandleFunc("/health", healthHandler)

	// Wrap with logging middleware
	handler := loggingMiddleware(mux)

	log.Printf("Starting server on port %s", port)

	server := &http.Server{
		Addr:    ":" + port,
		Handler: handler,
	}

	if err := server.ListenAndServe(); err != nil {
		log.Fatal("Server failed to start:", err)
	}
}
