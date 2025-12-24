package main

import (
	"fmt"
	"io"
	"log"
	"net/http"
	"net/url"
	"os"
	"time"

	"github.com/joho/godotenv"
)

func main() {
	err := godotenv.Load()
	if err != nil {
		fmt.Errorf("There was an error loading the dotenv file: %s\n", err.Error())
	}
	DOAPIKEY := os.Getenv("DOAPIKEY")
	BASEURL := os.Getenv("BASEURL")
	dropleturl, err := url.JoinPath(BASEURL, "/v2/droplets")
	if err != nil {
		fmt.Errorf("There was an error trying to create the droplet URL: %s\n", err.Error())
	}

	req, err := makeReq("GET", DOAPIKEY, dropleturl)
	if err != nil {
		log.Fatal(err)
	}

	client := &http.Client{Timeout: time.Second * 10}
	resp, err := client.Do(req)
	if err != nil {
		log.Fatalf("Error making request: %v", err)
	}
	defer resp.Body.Close() // Ensure the response body is closed

	// 4. Handle the response
	body, err := io.ReadAll(resp.Body)
	if err != nil {
		log.Fatalf("Error reading response body: %v", err)
	}

	fmt.Printf("Response Status Code: %d %s\n", resp.StatusCode, http.StatusText(resp.StatusCode))
	fmt.Printf("Response Status: %s\n", resp.Status)
	fmt.Printf("Response Body: %s\n", string(body))

}

func makeReq(method string, apikey string, url string) (*http.Request, error) {
	req, err := http.NewRequest(method, url, nil)
	if err != nil {
		return &http.Request{}, err
	}
	req.Header.Add("Authorization", apikey)
	req.Header.Add("Content-Type", "application/json")
	return req, nil
}
