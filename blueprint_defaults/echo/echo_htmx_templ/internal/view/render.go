package view

import (
	"github.com/a-h/templ"
	"github.com/labstack/echo/v4"
)

// func render(ctx echo.Context, statusCode int, t templ.Component) error {
// 	ctx.Response().Writer.WriteHeader(statusCode)
// 	ctx.Response().Header().Set(echo.HeaderContentType, echo.MIMETextHTML)
// 	return t.Render(ctx.Request().Context(), ctx.Response().Writer)
// }

// type TemplateRegistry struct {
// 	templates *templ.Component
// }

// func (t *TemplateRegistry) Render(w io.Writer, name string, data interface{}, c echo.Context) error {
// 	return t.templates.Render(w, name, data)
// }

func render(ctx echo.Context, statusCode int, cmp templ.Component) error {
	ctx.Response().Writer.WriteHeader(statusCode)
	ctx.Response().Header().Set(echo.HeaderContentType, echo.MIMETextHTML)
	return cmp.Render(ctx.Request().Context(), ctx.Response())
}
