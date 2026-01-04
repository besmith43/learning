package server

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"time"

	"example.com/cmd/web"
	"github.com/a-h/templ"
)

func (s *Server) RegisterRoutes() http.Handler {
	mux := http.NewServeMux()

	// Register routes
	mux.HandleFunc("/", s.HelloWorldHandler)

	mux.HandleFunc("/health", s.healthHandler)

	fileServer := http.FileServer(http.FS(web.Files))
	mux.Handle("/assets/", fileServer)
	mux.Handle("/web", templ.Handler(web.HelloForm()))
	mux.Handle("/dropdown", templ.Handler(web.Dropdown()))
	mux.Handle("/collapse", templ.Handler(web.Collapse()))
	mux.Handle("/nav_tabs", templ.Handler(web.Nav_Tabs()))
	mux.Handle("/toast", templ.Handler(web.Toast()))
	mux.HandleFunc("/hello", web.HelloWebHandler)
	mux.HandleFunc("/counter", web.CounterWebHandler)
	mux.HandleFunc("/plus", web.PlusWebHandler)
	mux.HandleFunc("/minus", web.MinusWebHandler)
	mux.Handle("/reset", templ.Handler(web.Reset()))

	// Chain middleware
	chain := chainMiddleware(
		loggingMiddleware,
		corsMiddleware,
	)

	return chain(mux)
}

// Middleware function that accepts http.Handler
type MiddlewareFunc func(http.Handler) http.Handler

func chainMiddleware(middlewares ...MiddlewareFunc) MiddlewareFunc {
	return func(next http.Handler) http.Handler {
		for i := len(middlewares) - 1; i >= 0; i-- {
			next = middlewares[i](next)
		}
		return next
	}
}

func corsMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		// Set CORS headers
		w.Header().Set("Access-Control-Allow-Origin", "*") // Replace "*" with specific origins if needed
		w.Header().Set("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH")
		w.Header().Set("Access-Control-Allow-Headers", "Accept, Authorization, Content-Type, X-CSRF-Token")
		w.Header().Set("Access-Control-Allow-Credentials", "false") // Set to "true" if credentials are required

		// Handle preflight OPTIONS requests
		if r.Method == http.MethodOptions {
			w.WriteHeader(http.StatusNoContent)
			return
		}

		// Proceed with the next handler
		next.ServeHTTP(w, r)
	})
}

func loggingMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		start := time.Now()
		fmt.Printf("Started %s %s for %s\n", r.Method, r.URL.Path, r.RemoteAddr)
		next.ServeHTTP(w, r)
		fmt.Printf("Completed %s in %v\n", r.URL.Path, time.Since(start))
	})
}

func (s *Server) HelloWorldHandler(w http.ResponseWriter, r *http.Request) {
	resp := map[string]string{"message": "Hello World"}
	jsonResp, err := json.Marshal(resp)
	if err != nil {
		http.Error(w, "Failed to marshal response", http.StatusInternalServerError)
		return
	}
	w.Header().Set("Content-Type", "application/json")
	if _, err := w.Write(jsonResp); err != nil {
		log.Printf("Failed to write response: %v", err)
	}
}

func (s *Server) healthHandler(w http.ResponseWriter, r *http.Request) {
	healthResp := map[string]string{"message": "Healthy"}
	resp, err := json.Marshal(healthResp)
	if err != nil {
		http.Error(w, "Failed to marshal health check response", http.StatusInternalServerError)
		return
	}
	w.Header().Set("Content-Type", "application/json")
	if _, err := w.Write(resp); err != nil {
		log.Printf("Failed to write response: %v", err)
	}
}
