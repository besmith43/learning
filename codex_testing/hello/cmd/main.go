package main

import (
	"bufio"
	"errors"
	"fmt"
	"os"
	"os/exec"
	"sort"
	"strings"
)

func main() {
	fmt.Println("Hello World")

	options := []string{
		"apple",
		"banana",
		"grape",
		"orange",
		"watermelon",
		"blueberry",
		"strawberry",
	}

	selected, err := fuzzy_select(options)
	if err != nil {
		fmt.Printf("Error: %v\n", err)
		return
	}

	fmt.Printf("You selected: %s\n", selected)
}

func fuzzy_select(options []string) (string, error) {
	if len(options) == 0 {
		return "", errors.New("no options provided")
	}

	if !isTTY() {
		return "", errors.New("fuzzy_select requires an interactive terminal")
	}

	restore, err := enableRawMode()
	if err != nil {
		return "", fmt.Errorf("failed to enable raw mode: %w", err)
	}
	defer func() { _ = restore() }()

	reader := bufio.NewReader(os.Stdin)
	query := ""
	selected := 0

	for {
		matches := rankOptions(options, query, 10)
		if selected >= len(matches) {
			selected = len(matches) - 1
		}
		if selected < 0 {
			selected = 0
		}

		renderFuzzyUI(query, matches, selected)

		b, err := reader.ReadByte()
		if err != nil {
			return "", err
		}

		switch b {
		case 3: // Ctrl-C
			fmt.Print("\x1b[2J\x1b[H")
			return "", errors.New("selection canceled")
		case '\r', '\n':
			if len(matches) == 0 {
				continue
			}
			fmt.Print("\x1b[2J\x1b[H")
			return matches[selected], nil
		case 127, 8: // Backspace/Delete
			if len(query) > 0 {
				query = query[:len(query)-1]
			}
		case 27: // Arrow keys come as ESC [ A/B
			b2, err := reader.ReadByte()
			if err != nil {
				continue
			}
			b3, err := reader.ReadByte()
			if err != nil {
				continue
			}
			if b2 == '[' {
				switch b3 {
				case 'A':
					if selected > 0 {
						selected--
					}
				case 'B':
					if selected < len(matches)-1 {
						selected++
					}
				}
			}
		default:
			if b >= 32 && b <= 126 {
				query += string(b)
			}
		}
	}
}

func isTTY() bool {
	fi, err := os.Stdin.Stat()
	if err != nil {
		return false
	}
	return (fi.Mode() & os.ModeCharDevice) != 0
}

func enableRawMode() (func() error, error) {
	save := exec.Command("stty", "-g")
	save.Stdin = os.Stdin
	stateBytes, err := save.Output()
	if err != nil {
		return nil, err
	}
	state := strings.TrimSpace(string(stateBytes))

	raw := exec.Command("stty", "raw", "-echo")
	raw.Stdin = os.Stdin
	if err := raw.Run(); err != nil {
		return nil, err
	}

	restore := func() error {
		cmd := exec.Command("stty", state)
		cmd.Stdin = os.Stdin
		return cmd.Run()
	}
	return restore, nil
}

func renderFuzzyUI(query string, matches []string, selected int) {
	fmt.Print("\x1b[2J\x1b[H")
	fmt.Printf("fuzzy> %s\r\n", query)
	fmt.Print("Type to filter, ↑/↓ to move, Enter to select, Ctrl-C to cancel\r\n\r\n")

	if len(matches) == 0 {
		fmt.Print("  (no matches)\r\n")
		return
	}

	for i, item := range matches {
		prefix := "  "
		if i == selected {
			prefix = "> "
		}
		fmt.Printf("%s%s\r\n", prefix, item)
	}
}

func rankOptions(options []string, query string, limit int) []string {
	type scored struct {
		value string
		score int
		index int
	}

	if limit <= 0 {
		limit = len(options)
	}

	query = strings.ToLower(query)
	scoredItems := make([]scored, 0, len(options))
	for i, option := range options {
		score, ok := fuzzyScore(strings.ToLower(option), query)
		if !ok {
			continue
		}
		scoredItems = append(scoredItems, scored{
			value: option,
			score: score,
			index: i,
		})
	}

	sort.Slice(scoredItems, func(i, j int) bool {
		if scoredItems[i].score == scoredItems[j].score {
			return scoredItems[i].index < scoredItems[j].index
		}
		return scoredItems[i].score > scoredItems[j].score
	})

	if len(scoredItems) > limit {
		scoredItems = scoredItems[:limit]
	}

	results := make([]string, 0, len(scoredItems))
	for _, item := range scoredItems {
		results = append(results, item.value)
	}
	return results
}

func fuzzyScore(candidate, query string) (int, bool) {
	if query == "" {
		return 0, true
	}

	pos := 0
	score := 0
	lastIdx := -1
	firstMatch := -1

	for _, qc := range query {
		idx := strings.IndexRune(candidate[pos:], qc)
		if idx == -1 {
			return 0, false
		}
		absIdx := pos + idx
		if firstMatch == -1 {
			firstMatch = absIdx
		}

		if lastIdx >= 0 {
			gap := absIdx - lastIdx - 1
			if gap == 0 {
				score += 10
			} else {
				score -= gap
			}
		}

		score += 5
		lastIdx = absIdx
		pos = absIdx + 1
	}

	score -= firstMatch
	score -= len(candidate) / 8
	return score, true
}
