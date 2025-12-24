package main

import (
	"context"
	"database/sql"
	"fmt"
	"log"
	"time"

	_ "github.com/go-sql-driver/mysql"
	"github.com/sethvargo/go-retry"
)

// example taken from: https://github.com/sethvargo/go-retry

func main() {

	fmt.Println("This example will retry indefinitely")
	fmt.Println("press Ctrl+C when you're ready to quit")

	db, err := sql.Open("mysql", "user:password@/dbname")
	if err != nil {
		log.Fatal(err)
	}

	ctx := context.Background()
	if err := retry.Fibonacci(ctx, 1*time.Second, func(ctx context.Context) error {
		if err := db.PingContext(ctx); err != nil {
			// This marks the error as retryable
			fmt.Println("retrying")
			return retry.RetryableError(err)
		}
		return nil
	}); err != nil {
		log.Fatal(err)
	}
}
