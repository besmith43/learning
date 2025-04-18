package main

import (
	"fmt"
	"log"
	"net/http"
)

func main() {
	fmt.Println("listening on port 3000")

	http.HandleFunc("/foo", fooHandler)

	log.Fatal(http.ListenAndServe(":3000", nil))
}

func fooHandler(w http.ResponseWriter, r *http.Request) {

	fmt.Println("received request to foo")

	hardcodedOutput := `Store: Vending machine, Amount: 3.100000, ID: 1
Store: Hashtag gaming, Amount: 32.500000, ID: 2
Store: Linode , Amount: 10.000000, ID: 3
Store: Vending machine , Amount: 2.200000, ID: 4
Store: Amazon, Amount: 29.320000, ID: 5
Store: Apple, Amount: 49.980000, ID: 6
Store: Subway, Amount: 14.460000, ID: 7
Store: Mcdonalds , Amount: 6.030000, ID: 8
Store: Walmart, Amount: 39.180000, ID: 9
Store: Hashtag gaming, Amount: 3.180000, ID: 10
Store: Wendys , Amount: 7.410000, ID: 11
Store: Vending machine, Amount: 2.200000, ID: 12
Store: Pho, Amount: 14.460000, ID: 14
Store: Walmart, Amount: 39.180000, ID: 15
Store: McDonalds, Amount: 6.030000, ID: 16
Store: HashTag Gaming Arena, Amount: 3.180000, ID: 17
Store: Wendys, Amount: 7.410000, ID: 18
Store: Target, Amount: 9.180000, ID: 19
Store: United Airlines, Amount: 363.970000, ID: 20
Store: MidnighTreats, Amount: 7.680000, ID: 21
Store: Gruns, Amount: 71.590000, ID: 22
Store: Airport Convenient Store, Amount: 8.970000, ID: 23
Store: Chick-Fil-A, Amount: 13.520000, ID: 24
Store: Vending machine, Amount: 2.200000, ID: 25
Store: Vending machine, Amount: 1.500000, ID: 26
Store: Walmart, Amount: 72.620000, ID: 27
Store: McDonalds, Amount: 8.670000, ID: 28
Store: EZPass VA, Amount: 3.600000, ID: 29
Store: Apple, Amount: 9.990000, ID: 30
Store: Apple, Amount: 2.990000, ID: 31
Store: King Buffet, Amount: 19.070000, ID: 32
Store: Amazon, Amount: 154.990000, ID: 33
Store: Amazon, Amount: 120.820000, ID: 34`
	fmt.Fprintf(w, hardcodedOutput)
}
