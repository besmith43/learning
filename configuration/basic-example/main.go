package main

import (
	"fmt"

	gocfg "github.com/dsbasko/go-cfg"
)

type Config struct {
	Mode string `default:"prod" json:"mode" yaml:"mode" s-flag:"m" flag:"mode" env:"MODE" description:"mode of the application (dev|prod)"`
	HTTP struct {
		Host         string `default:"localhost" json:"host" yaml:"host" s-flag:"h" flag:"http-host" env:"HTTP_HOST"`
		Port         int    `default:"3000" json:"port" yaml:"port" s-flag:"p" flag:"http-port" env:"HTTP_PORT"`
		ReadTimeout  int    `json:"read_timeout" yaml:"read-timeout" flag:"http-read-timeout" env:"HTTP_READ_TIMEOUT"`
		WriteTimeout int    `json:"write_timeout" yaml:"write-timeout" flag:"http-write-timeout" env:"HTTP_WRITE_TIMEOUT"`
	} `json:"http" yaml:"http"`
}

func main() {
	cfg := Config{}

	gocfg.MustReadFile("configs/config.yaml", &cfg)
	gocfg.MustReadEnv(&cfg)
	gocfg.MustReadFlag(&cfg)

	fmt.Println(cfg.Mode)
	fmt.Println(cfg.HTTP.Host)
	fmt.Println(cfg.HTTP.Port)
	fmt.Println(cfg.HTTP.ReadTimeout)
	fmt.Println(cfg.HTTP.WriteTimeout)
}
