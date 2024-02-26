package main

import (
	"data_integration_clis/internal/models/hsi"
	"fmt"
)

func main() {
	fmt.Println("hello from hsi")

	worker := hsi.Employee{Fname: "Jim", Lname: "Kirk", Tnumber: "T00000003", Jobcode: "Captian"}

	fmt.Printf("Employee's fname: %s lname: %s tnumber: %s jobcode: %s\n", worker.Fname, worker.Lname, worker.Tnumber, worker.Jobcode)

	sd := hsi.SharedData{ServiceName: "suitable"}

	fmt.Println("Service Name:", sd.ServiceName)
}
