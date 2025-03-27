package main

import "fmt"

type Shape interface {
	Area() float64
	Perimeter() float64
}

type Circle struct {
	Radius float64
}

func (c Circle) Area() float64 {
	return 3.14159 * c.Radius * c.Radius
}

func (c Circle) Perimeter() float64 {
	return 2 * 3.14159 * c.Radius
}

type Rectangle struct {
	Width  float64
	Height float64
}

func (r Rectangle) Area() float64 {
	return r.Width * r.Height
}

func (r Rectangle) Perimeter() float64 {
	return 2 * (r.Width + r.Height)
}

func main() {
	c := Circle{Radius: 5}
	r := Rectangle{Width: 4, Height: 6}

	printShapeInfo(c) // Output: Area: 78.53975, Perimeter: 31.4159
	printShapeInfo(r) // Output: Area: 24, Perimeter: 20
}

func printShapeInfo(s Shape) {
	fmt.Printf("Area: %f, Perimeter: %f\n", s.Area(), s.Perimeter())
}
