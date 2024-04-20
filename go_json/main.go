package main

import (
	"encoding/json"
	"fmt"
	"log"
	"os"
	"time"
)

// Employee3 class example
type Employee3 struct {
	Name     string     `json:"name"`
	Age      int        `json:"age"`
	Salary   int        `json:"salary"`
	Employee *Employee3 `json:"employee"`
	Other    *Other     `json:"other"`
}

// Other structure
type Other struct {
	Tagada string `json:"tagada,omitempty"`
}

type Person struct {
	Firstname string `json:"Firstname"`
	Lastname  string `json:"Lastname"`
	Age       int    `json:"Age"`
	Birthday  string `json:"Birthday"`
}

func main() {
	fmt.Println("Hello world")

	empobj3 := Employee3{Name: "", Age: 24, Salary: 344444}
	emp3, _ := json.Marshal(empobj3)
	fmt.Println(string(emp3))

	dat, err := os.ReadFile("./example1.json")
	if err != nil {
		log.Fatal(err)
	}

	jsonData := []byte(dat)

	var person Person

	json.Unmarshal(jsonData, &person)

	fmt.Printf("Person Data: %s, %s, %d, %s\n", person.Firstname, person.Lastname, person.Age, person.Birthday)

	what := time.Now()
	fmt.Printf("%d\n", what.Year())

	const shortForm = "2006-Jan-02"
	t, _ := time.Parse(shortForm, "2013-Feb-03")
	fmt.Println(t)

	// const shortForm = "1999-Feb-25"
	// bday, err := time.Parse(shortForm, person.Birthday)
	bday, err := time.Parse(shortForm, "2004-May-02")
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println(bday)
	fmt.Printf("%d %d %d\n", bday.Year(), bday.Month(), bday.Day())
}
