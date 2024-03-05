package main

import (
	"context"
	"fmt"
)

func main() {
	cfg, err := myconfig.LoadFromPath(context.Background(), "config.pkl")
	if err != nil {
		panic(err)
	}
	fmt.Printf("I'm running on host %s\n", cfg.Host)
}
