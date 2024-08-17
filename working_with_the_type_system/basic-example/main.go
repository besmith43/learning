package main

import "fmt"

// examples 1-3 came from the following video:
// https://www.youtube.com/watch?v=pCA7oTBH4Zk&t=789s

// example 4 came from this blog post:
// https://www.fullstory.com/blog/is-go-duck-typed/

// example 2
type Base interface {
	Foo()
}

type MyStruct struct{}

func (m *MyStruct) Foo() {}
func (m *MyStruct) Bar() {
	fmt.Println("Bar is running")
}

// end example 2

// example 3
func process[T any](value T) {
	switch v := any(value).(type) {
	case int:
		fmt.Println("Integer:", v)
	case string:
		fmt.Println("String:", v)
	default:
		fmt.Println("Unknown:", v)
	}
}

// end example 3

// example 4
type Animal interface {
	NumberOfLegs() int
}

type Spider struct{}

func (s Spider) NumberOfLegs() int {
	return 8
}

func (s Spider) Genus() string {
	return "Latrodectus" //strictly speaking, the genus of Black Widow spider
}

type Dog struct{}

func (d Dog) NumberOfLegs() int {
	return 4
}
func (d Dog) Genus() string {
	return "Canin"
}

type Stool struct{}

func (s Stool) NumberOfLegs() int {
	return 3
}

// end example 4

func main() {
	fmt.Println("Hello World!")

	// first example

	// same as var a any = "Hello"
	var a interface{} = "Hello" // a can have any type

	// b := a.(string) // casting A as a string to assignment to B

	// this can be used to protect against a being set to something that isn't a string
	if b, ok := a.(string); ok {
		fmt.Println(b)
	}

	a = 3

	c := a.(int)

	fmt.Println(c)

	// second example

	var i Base = &MyStruct{}

	// read as
	// if i implements an interface with the function Bar(), then
	if v, ok := i.(interface{ Bar() }); ok {
		v.Bar()
	}

	// third example
	// Type Switch
	// also makes use of generics

	d := 3

	process(d)

	e := 3.0

	process(e)

	// example 4
	// this is one of my own making to test the claim that I've heard that go's type system only cares if an shape is met
	// my understanding is basically duck typing

	spy := Spider{}

	fmt.Printf("Spy's Type: %T\n", spy)

	spot := Dog{}

	fmt.Printf("Spot's Type: %T\n", spot)

	group := [...]Animal{spy, spot}

	fmt.Printf("The Group's Type: %T\n", group)
}
