package main

import (
	"fmt"
	"os"
)

func Init(initDir string, initProjectName string, initBare bool) (bool, error) {
	fmt.Println("Starting directory:", initDir)
	fmt.Println("Project Name:", initProjectName)
	fmt.Println("Bare Init:", initBare)

	if initDir[len(initDir)-1:] == "/" {
		// fmt.Println("Trimming leading /")
		initDir = initDir[0 : len(initDir)-1]
		// fmt.Printf("New initial directory: %s\n", initDir)
	}

	err := os.MkdirAll(fmt.Sprintf("%s/%s/PreScripts", initDir, initProjectName), 0755)
	if err != nil {
		return false, err
	}

	err = os.MkdirAll(fmt.Sprintf("%s/%s/PostScripts", initDir, initProjectName), 0755)
	if err != nil {
		return false, err
	}
	err = os.MkdirAll(fmt.Sprintf("%s/%s/Tables", initDir, initProjectName), 0755)
	if err != nil {
		return false, err
	}

	err = os.MkdirAll(fmt.Sprintf("%s/%s/Views", initDir, initProjectName), 0755)
	if err != nil {
		return false, err
	}

	err = os.MkdirAll(fmt.Sprintf("%s/%s/StoredProcedures", initDir, initProjectName), 0755)
	if err != nil {
		return false, err
	}

	if initBare {
		widgetsTable := []byte("Create table Widget (WidgetID int, WidgetName varchar(50))")
		err := os.WriteFile(fmt.Sprintf("%s/%s/Tables/Widgets.sql", initDir, initProjectName), widgetsTable, 0755)
		if err != nil {
			return false, err
		}

		exampleWidgets := []byte(`INSERT INTO Widget (WidgetID, WidgetName)
		VALUES (1, 'Bob'), (2, 'Sue'), (3, 'Sally'), (4, null)`)
		err = os.WriteFile(fmt.Sprintf("%s/%s/PostScripts/ExampleWidgets.sql", initDir, initProjectName), exampleWidgets, 0755)
		if err != nil {
			return false, err
		}
	}

	return true, nil
}
