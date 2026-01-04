package view

import (
	"github.com/labstack/echo/v4"
)

func HelloWebHandler(c echo.Context) error {
	return Render(c, HelloForm())
}

func HelloPostHandler(c echo.Context) error {
	name := c.FormValue("name")
	return Render(c, HelloPost(name))
}
