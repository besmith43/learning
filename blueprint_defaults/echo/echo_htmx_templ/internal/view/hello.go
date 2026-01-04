package view

import (
	"github.com/labstack/echo/v4"
)

func HelloWebHandler(c echo.Context) error {
	return render(c, 200, HelloForm())
}

func HelloPostHandler(c echo.Context) error {
	name := c.FormValue("name")
	return render(c, 200, HelloPost(name))
}
