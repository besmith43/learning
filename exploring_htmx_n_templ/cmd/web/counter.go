package web

import (
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"strconv"
	"strings"
)

func CounterWebHandler(w http.ResponseWriter, r *http.Request) {
	var count int = 0

	component := Counter(count)
	err := component.Render(r.Context(), w)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		log.Fatalf("Error rendering in HelloWebHandler: %e", err)
	}
}

func PlusWebHandler(w http.ResponseWriter, r *http.Request) {
	acceptHeader := r.Header.Get("Accept")
	fmt.Println("Accept Header: ", acceptHeader)

	body, err := ioutil.ReadAll(r.Body)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	defer r.Body.Close()

	fmt.Println("Request Body:", string(body))

	tmp := strings.Split(string(body), "=")

	count, err := strconv.Atoi(tmp[1])
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	count += 1

	component := Plus(count)
	err = component.Render(r.Context(), w)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		log.Fatalf("Error rendering in HelloWebHandler: %e", err)
	}
}

func MinusWebHandler(w http.ResponseWriter, r *http.Request) {
	acceptHeader := r.Header.Get("Accept")
	fmt.Println("Accept Header: ", acceptHeader)

	body, err := ioutil.ReadAll(r.Body)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	defer r.Body.Close()

	fmt.Println("Request Body:", string(body))

	tmp := strings.Split(string(body), "=")

	count, err := strconv.Atoi(tmp[1])
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	count -= 1

	if acceptHeader == "application/json" && count < -5 {
		http.Error(w, "this endpoint doesn't return json when the count is less than -5", http.StatusBadRequest)
		return
	}

	component := Plus(count)
	err = component.Render(r.Context(), w)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		log.Fatalf("Error rendering in HelloWebHandler: %e", err)
	}
}
