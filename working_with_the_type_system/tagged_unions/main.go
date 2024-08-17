package main

import "fmt"

// this is coming from the following reddit post:
// https://www.reddit.com/r/golang/comments/13hjevf/idiomatic_way_in_go_to_represent_a_tagged_union/

// problem:
// golang doesn't have tagged unions types
// meaning that you can't just have multiple various types grouped together
// this is a common problem with api's as json, aka javascript, can and often does

/*
	rust solution

	pub enum Email {
		None,
		Verified(String),
		Unverified(String, String)
	}
*/

/*
	typescript solution

	type Email =
	{
		status: 'None'
	} |
	{
		status: 'Verified',
		emailAddress: string
	} |
	{
		status: 'Unverified',
		emailAddress: string,
		verificationCode: string
	}
*/

// go solutions

/*
	// union of fields

	type EmailStatus string

	const (
		EmailStatusNone EmailStatus = "none"
		EmailStatusVerified EmailStatus = "verified"
		EmailStatusUnverified EmailStatus = "unverified"
	)

	type EmailDetails struct {
		Status EmailStatus
		EmailAddress string
		VerificationCode string
	}
*/

/*
	// structs with shared interface

	type StatusNone struct {
	}
	func (s StatusNone) Status() EmailStatus { return EmailStatusNone }

	type StatusUnverified struct {
		EmailAddress string
	}

	func (s StatusUnverified) Status() EmailStatus { return EmailStatusUnverified }

	type StatusVerified struct {
		EmailAddress string
		VerificationCode string
	}

	func (s StatusVerified) Status() EmailStatus { return EmailStatusVerified }

	type Email interface {
		Status() EmailStatus
	}
*/

/*
	// "fat" union

	type StatusNone struct {
	}

	type StatusUnverified struct {
		EmailAddress string
	}

	type StatusVerified struct {
		EmailAddress string
		VerificationCode string
	}

	type Email struct {
		StatusNone
		StatusUnverified
		StatusVerified
		Status EmailStatus
	}
*/

// golang does have enums, but they don't seem to be useful like I'm accustomed
// https://builtin.com/software-engineering-perspectives/golang-enum
// it seems like for what I'd want, a const is the right answer

func main() {
	fmt.Println("Hello World!")
}
