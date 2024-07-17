package main

import (
	"fmt"
	"os"

	"github.com/charmbracelet/huh"
)

func main() {

	dirs, err := os.ReadDir("example_dir")
	if err != nil {
		fmt.Println("read dir error: ", err)
		os.Exit(1)
	}

	var dirOptions []huh.Option[string]

	for _, dir := range dirs {
		dirName := dir.Name()
		dirOptions = append(dirOptions, huh.NewOption(dirName, dirName))
	}

	// fmt.Println(string_dirs)

	var selected_dirs []string
	var toppings []string
	// var topping string
	// value := "Dynamic"

	var list_toppings = [...]string{"Lettuce", "Tomatoes", "Charm Sauce", "Jalapenos", "Cheese", "Vegan Cheese", "Nutella"}

	var test_options []huh.Option[string]

	for _, top := range list_toppings {
		test_options = append(test_options, huh.NewOption(top, top))
	}

	// for _, topOption := range test_options {
	// 	fmt.Println("Option Key: ", topOption.Key)
	// 	fmt.Println("Option Value: ", topOption.Value)
	// }

	form := huh.NewForm(
		huh.NewGroup(
			huh.NewMultiSelect[string]().
				Options(test_options...).
				Title("Toppings").
				Limit(4).
				Value(&toppings),
		),

		// huh.NewGroup(
		// 	huh.NewMultiSelect[string]().
		// 		Options(
		// 			huh.NewOption("Lettuce", "Lettuce").Selected(true),
		// 			huh.NewOption("Tomatoes", "Tomatoes").Selected(true),
		// 			huh.NewOption("Charm Sauce", "Charm Sauce"),
		// 			huh.NewOption("Jalapeños", "Jalapeños"),
		// 			huh.NewOption("Cheese", "Cheese"),
		// 			huh.NewOption("Vegan Cheese", "Vegan Cheese"),
		// 			huh.NewOption("Nutella", "Nutella"),
		// 		).
		// 		Title("Toppings").
		// 		Limit(4).
		// 		Value(&toppings),
		// ),

		huh.NewGroup(
			huh.NewMultiSelect[string]().
				Options(dirOptions...).
				Title("Directories").
				Limit(4).
				Value(&selected_dirs),
		),
	)

	err = form.Run()

	if err != nil {
		fmt.Println("Uh oh:", err)
		os.Exit(1)
	}

	// fmt.Println(topping)
	fmt.Println(toppings)
	// fmt.Println(selected_dirs)

	for _, dir := range selected_dirs {
		fmt.Println("Performing an operation on", dir)
	}
}
