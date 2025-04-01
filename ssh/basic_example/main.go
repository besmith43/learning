package main

import (
	"fmt"
	"log"
	"os"

	"github.com/melbahja/goph"
)

func main() {

	homePath := os.Getenv("HOME")
	ssh_key := fmt.Sprintf("%s/.ssh/id_rsa", homePath)

	_, err := os.Stat(ssh_key)
	if err != nil {
		panic(err)
	}

	// Start new ssh connection with private key.
	auth, err := goph.Key(ssh_key, "")
	if err != nil {
		log.Fatal(err)
	}

	client, err := goph.New("besmith", "besmith.synology.me", auth)
	if err != nil {
		log.Fatal(err)
	}

	// Defer closing the network connection.
	defer client.Close()

	// Execute your command.
	out, err := client.Run("ls /tmp/")

	if err != nil {
		log.Fatal(err)
	}

	// Get your output as []byte.
	fmt.Println(string(out))
}
