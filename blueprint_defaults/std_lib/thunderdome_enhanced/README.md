# Project thunderdome_enhanced

One Paragraph of project description goes here

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

## MakeFile

run all make commands with clean tests
```bash
make all build
```

build the application
```bash
make build
```

run the application
```bash
make run
```

Create DB container
```bash
make docker-run
```

Shutdown DB container
```bash
make docker-down
```

live reload the application
```bash
make watch
```

run the test suite
```bash
make test
```

clean up binary from the last build
```bash
make clean
```



 Next steps:
 • cd into the newly created project with: `cd thunderdome_enhanced`

 • Install the templ cli if you haven't already by running `go install github.com/a-h/templ/cmd/templ@latest`

 • Generate templ function files by running `templ generate`

 Tip: Repeat the equivalent Blueprint with the following non-interactive command:
 • go-blueprint create --name thunderdome_enhanced --framework standard-library --driver sqlite --advanced true


