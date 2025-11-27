package main

import (
	"fmt"
	"io"
	"os"

	"github.com/vbauerster/mpb/v8"
	"github.com/vbauerster/mpb/v8/decor"
)

func main() {
	srcPath := "source.txt"
	dstPath := "destination.txt"

	// Create a dummy source file for demonstration
	createDummyFile(srcPath, 100*1024*1024) // 100MB file

	// Open source file
	src, err := os.Open(srcPath)
	if err != nil {
		fmt.Println("Error opening source file:", err)
		return
	}
	defer src.Close()

	// Create destination file
	dst, err := os.Create(dstPath)
	if err != nil {
		fmt.Println("Error creating destination file:", err)
		return
	}
	defer dst.Close()

	// Get source file info to get size
	srcInfo, err := src.Stat()
	if err != nil {
		fmt.Println("Error getting source file info:", err)
		return
	}
	totalSize := srcInfo.Size()

	// Create a new progress container
	p := mpb.New(mpb.WithWidth(60))

	// Add a new bar to the progress container
	bar := p.AddBar(totalSize,
		mpb.PrependDecorators(
			decor.CountersKibiByte("% .2f / % .2f"),
		),
		mpb.AppendDecorators(
			decor.Percentage(decor.WCSyncSpace),
			decor.EwmaETA(decor.ET_STYLE_GO, 90),
		),
	)

	// Create a proxy reader to update the progress bar
	proxyReader := bar.ProxyReader(src)
	defer proxyReader.Close()

	// Copy content from source to destination
	if _, err := io.Copy(dst, proxyReader); err != nil {
		fmt.Println("Error copying file:", err)
		return
	}

	// Wait for the bar to complete and then remove the source file
	p.Wait()
	fmt.Println("\nFile copied successfully.")

	if err := os.Remove(srcPath); err != nil {
		fmt.Println("Error removing source file:", err)
		return
	}
	fmt.Println("Source file removed.")
}

// createDummyFile creates a file of a specified size for testing
func createDummyFile(path string, size int64) {
	f, err := os.Create(path)
	if err != nil {
		panic(err)
	}
	defer f.Close()
	_, err = f.Write(make([]byte, size))
	if err != nil {
		panic(err)
	}
}
