package main

import (
	"data_integration_clis/internal/models/suitable"
	"fmt"
)

func main() {
	fmt.Println("hello from suitable")

	kid := suitable.Student{Fname: "Bob", Lname: "Smith", Tnumber: "T00000001"}

	fmt.Printf("Student fname: %s lname: %s tnumber: %s\n", kid.Fname, kid.Lname, kid.Tnumber)

	sd := suitable.SharedData{ServiceName: "suitable"}

	fmt.Println("Service Name:", sd.ServiceName)
}
