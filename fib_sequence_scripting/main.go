package main

import (
	"fmt"
	"os"
	"strconv"
)

func main() {
    argsWithoutProg := os.Args[1:]

    if len(argsWithoutProg) != 1 {
        fmt.Println("you need to pass in a number")
        os.Exit(1)
    }

    x, err := strconv.Atoi(argsWithoutProg[0])
    if err != nil {
        fmt.Println("the argument wasn't a number")
        os.Exit(1)
    }

    answer := calculate_fib(x)

    fmt.Println(answer)
}

func calculate_fib(num int) int {
    
    if num == 0 {
        return 1
    }

    if num == 1 {
        return 2
    }

    x := 1
    y := 1
    sum := 0

    for i := 2; i < num; i++ {
        sum = x + y
        x = y
        y = sum
    }

    return sum
}
