package main

import (
	"flag"
	"fmt"
	"log"
	"net/http"
	"os"
	"plugin"
	"strconv"
	"strings"
)

func loadPlugin() func() string {
	// plugin, err := plugin.Open("plugins/simple-plugin.so")
	plugin, err := plugin.Open(pluginPath)
	if err != nil {
		log.Fatal(err)
	}

	simplePluginFunc, err := plugin.Lookup("SimplePluginFunc")
	if err != nil {
		log.Fatal(err)
	}

	f, ok := simplePluginFunc.(func() string)
	if !ok {
		log.Fatal("unexpected type from module symbol")
	}

	return f
}

func handler(w http.ResponseWriter, r *http.Request) {
	simplePlugin := loadPlugin()

	response := simplePlugin()

	fmt.Printf("response value: %s\n", response)
	fmt.Fprintf(w, "response value: %s\n", response)

	// fmt.Fprintf(w, "Hi there, I love %s!", r.URL.Path[1:])
}

var port int
var pluginPath string

func main() {
	confPtr := flag.String("conf", "", "path to conf")

	flag.Parse()

	data, err := os.ReadFile(*confPtr)
	if err != nil {
		log.Fatal(err)
	}

	contents := string(data[:])

	contentArr := strings.Split(contents, "\n")

	for _, line := range contentArr {
		if strings.Contains(line, "port") {
			portArr := strings.Split(line, "=")
			port, err = strconv.Atoi(portArr[1])
			if err != nil {
				log.Fatal(err)
			}
		} else if strings.Contains(line, "path") {
			pluginPathArr := strings.Split(line, "=")
			pluginPath = pluginPathArr[1]
		}
	}

	// port := 8080

	fmt.Println(pluginPath)

	http.HandleFunc("/", handler)
	fmt.Printf("listening on port %d\n", port)
	log.Fatal(http.ListenAndServe(fmt.Sprintf(":%d", port), nil))
}
