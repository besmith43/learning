package main

import (
	"fmt"
	"os"
	"time"

	"atomicgo.dev/cursor"
	"golang.org/x/term"
)

func main() {
	area := cursor.NewArea()
	ClearAll(area)
	GridUpdate(area)
}

func main2() {
	fmt.Println("Cursor area movement demo")
	fmt.Println("--------------------------")

	area := cursor.NewArea()
	content := `Start content with some rows
	1. Row1
	2. Row2
	---
	`
	area.Update(content)

	time.Sleep(1 * time.Second)
	area.Up(2)
	area.Move(3, 0)
	fmt.Print("Replaced row 2")

	time.Sleep(1 * time.Second)
	area.StartOfLine()
	area.Move(8, -1)
	fmt.Print("3. Appended row")

	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after move)")

	time.Sleep(1 * time.Second)
	area.Up(6)
	fmt.Print("<<< AFTER Up(6)")
	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after cursor up out of bounds)")

	time.Sleep(1 * time.Second)
	area.Down(6)
	fmt.Print("<<< AFTER Down(6)")
	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after cursor down out of bounds)")

	time.Sleep(1 * time.Second)
	area.Top()
	fmt.Print("<<< AFTER Top()")
	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after cursor top)")

	time.Sleep(1 * time.Second)
	area.Bottom()
	fmt.Print("<<< AFTER Bottom()")
	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after cursor bottom)")

	time.Sleep(1 * time.Second)
	area.Update("")
	time.Sleep(1 * time.Second)
	area.Update(content + "(restored content after empty line)")

	time.Sleep(1 * time.Second)
	fmt.Println("\n--- DONE")

	GridUpdate(area)
}

func GridUpdate(area cursor.Area) {
	var gameMap [8]string
	gameMap[0] = "------------------------------"
	gameMap[1] = "|                            |"
	gameMap[2] = "|                            |"
	gameMap[3] = "|    <E>                     |"
	gameMap[4] = "|                            |"
	gameMap[5] = "|                            |"
	gameMap[6] = "|                            |"
	gameMap[7] = "------------------------------"

	var statusSidebar [8]string
	statusSidebar[0] = fmt.Sprintf("Position: %d,%d\n", 5, 3)
	statusSidebar[1] = fmt.Sprintf("Torpedoes: %d\n", 10)
	statusSidebar[2] = fmt.Sprintf("Health: %d\n", 100)
	statusSidebar[3] = fmt.Sprintf("\n")
	statusSidebar[4] = fmt.Sprintf("\n")
	statusSidebar[5] = fmt.Sprintf("\n")
	statusSidebar[6] = fmt.Sprintf("\n")
	statusSidebar[7] = fmt.Sprintf("\n")

	mainMenu := ""

	mainMenu = fmt.Sprintf("%stext in the main menu\n", mainMenu)
	mainMenu = fmt.Sprintf("%stext in the main menu\n", mainMenu)
	mainMenu = fmt.Sprintf("%stext in the main menu\n", mainMenu)
	mainMenu = fmt.Sprintf("%stext in the main menu\n", mainMenu)
	mainMenu = fmt.Sprintf("%stext in the main menu\n", mainMenu)

	// build contents

	area.Top()

	area.Update(fmt.Sprintf("%s    %s", gameMap[0], statusSidebar[0]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[1], statusSidebar[1]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[2], statusSidebar[2]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[3], statusSidebar[3]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[4], statusSidebar[4]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[5], statusSidebar[5]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[6], statusSidebar[6]))
	area.Down(1)
	area.Update(fmt.Sprintf("%s    %s", gameMap[7], statusSidebar[7]))
	area.Down(1)
	area.Down(1)

	area.Update(mainMenu)
}

func ClearAll(area cursor.Area) {
	_, height, err := term.GetSize(int(os.Stdout.Fd()))
	if err != nil {
		panic(fmt.Sprintf("Error getting console size:", err))
	} else {
		fmt.Printf("Height: %d\n", height)
	}

	area.Top()

	area.ClearLinesDown(height)

	area.Top()
}
