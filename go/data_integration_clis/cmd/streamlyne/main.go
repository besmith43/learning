package main

import (
	"data_integration_clis/internal/models/streamlyne"
	"fmt"
)

func main() {
	fmt.Println("hello from streamlyne")

	individual := streamlyne.Person{Fname: "Sally", Lname: "Storm", Tnumber: "T00000002"}

	fmt.Printf("Person's fname: %s lname: %s tnumber: %s\n", individual.Fname, individual.Lname, individual.Tnumber)

	sd := streamlyne.SharedData{ServiceName: "suitable"}

	fmt.Println("Service Name:", sd.ServiceName)
}
