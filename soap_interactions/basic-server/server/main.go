package main

import (
	"encoding/xml"
	"fmt"
	"net/http"

	"github.com/globusdigital/soap"
)

// FooRequest a simple request
type FooRequest struct {
	XMLName xml.Name `xml:"fooRequest"`
	Foo     string
}

// FooResponse a simple response
type FooResponse struct {
	Bar string
}

// RunServer run a little demo server
func RunServer() {
	soapServer := soap.NewServer()
	soapServer.RegisterHandler(
		"/pathTo",
		// SOAPAction
		"operationFoo",
		// tagname of soap body content
		"fooRequest",
		// RequestFactoryFunc - give the server sth. to unmarshal the request into
		func() interface{} {
			return &FooRequest{}
		},
		// OperationHandlerFunc - do something
		func(request interface{}, w http.ResponseWriter, httpRequest *http.Request) (response interface{}, err error) {
			fooRequest := request.(*FooRequest)
			fooResponse := &FooResponse{
				Bar: "Hello " + fooRequest.Foo,
			}
			response = fooResponse
			return
		},
	)
	fmt.Println("server listening on port 8080")
	err := http.ListenAndServe(":8080", soapServer)
	fmt.Println("exiting with error", err)
}

func main() {
	RunServer()
}
