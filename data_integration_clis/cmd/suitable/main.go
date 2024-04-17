package main

import (
	"fmt"
	"log"
)

func main() {
	fmt.Println("hello from suitable")

	kid := Student{Fname: "Bob", Lname: "Smith", Tnumber: "T00000001"}

	fmt.Printf("Student fname: %s lname: %s tnumber: %s\n", kid.Fname, kid.Lname, kid.Tnumber)

	sd := SharedData{ServiceName: "suitable"}

	fmt.Println("Service Name:", sd.ServiceName)

	file, err := GetEmbeddedGopher()
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println(file)
}
