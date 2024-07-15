package main

import (
	"context"
	"fmt"

	"pkl_config/appconfig"

	_ "github.com/apple/pkl-go/pkl"
)

/*
type MyAppConfig struct {
	// The hostname for the application
	Host string `pkl:"host"`

	// The port to listen on
	Port uint16 `pkl:"port"`
}
*/

func main() {
	// cfg, err := appconfig.LoadFromPath(context.Background(), "AppConfig.pkl")
	// cfg, err := appconfig.LoadFromPath(context.Background(), "pkl/local/appConfig.pkl")
	cfg, err := appconfig.LoadFromPath(context.Background(), "pkl/prod/appConfig.pkl")
	if err != nil {
		panic(err)
	}
	fmt.Printf("I'm running on host %s\n", cfg.Host)
}
