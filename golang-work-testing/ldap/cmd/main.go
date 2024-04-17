package main

import (
	"fmt"
	"log"
	"strings"
	"syscall"

	"github.com/go-ldap/ldap"
	"golang.org/x/term"
)

var (
	ldapURL    string = "ldap://one.tntech.edu:389"
	searchUser string = "blakesmith"
	baseDN     string = "DC=tntech,DC=edu"
	filter     string = fmt.Sprintf("(CN=%s)", ldap.EscapeFilter(searchUser))
)

/*
	see these links for more information on the ldap package
	https://cybernetist.com/2020/05/18/getting-started-with-go-ldap/

	https://pkg.go.dev/gopkg.in/ldap.v3
*/

func main() {
	conn, err := ldap.DialURL(ldapURL)
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	// this came from stackoverflow
	// https://stackoverflow.com/questions/2137357/getpasswd-functionality-in-go
	fmt.Print("Enter Password for blakesmith2: ")
	bytePassword, err := term.ReadPassword(int(syscall.Stdin))
	if err != nil {
		log.Fatal(err)
	}

	password := string(bytePassword)

	err = conn.Bind("CN=blakesmith2,OU=Privileged 2 Accounts,OU=Faculty-Staff,OU=TTU Users,DC=tntech,DC=edu", strings.TrimSpace(password))
	if err != nil {
		log.Fatal(err)
	}

	searchReq := ldap.NewSearchRequest(baseDN, ldap.ScopeWholeSubtree, 0, 0, 0, false, filter, []string{"sAMAccountName", "MemberOf"}, []ldap.Control{})

	result, err := conn.Search(searchReq)
	if err != nil {
		log.Fatal("failed to query LDAP: %w", err)
	}

	log.Println("Got", len(result.Entries), "search results")

	for _, entry := range result.Entries {
		log.Println(entry.GetAttributeValue("sAMAccountName"))

		groupMembership := entry.GetAttributeValues("memberOf")
		for _, group := range groupMembership {
			log.Println(group)
		}

		fmt.Println("Group Count: ", len(groupMembership))
	}

}
