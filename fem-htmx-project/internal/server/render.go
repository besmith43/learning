package server

import (
	"html/template"
	"io"

	"github.com/labstack/echo/v4"
	"theprimeagen.tv/htmx/internal/views"
)

type Template struct {
	tmpl *template.Template
}

func newTemplate() *Template {
	// fmt.Println("Checking embed files")
	// content, err := views.Files.ReadFile("assets/index.html")
	// if err != nil {
	// 	log.Fatal(err)
	// }
	// fmt.Println("index.html : ", content)

	return &Template{
		tmpl: template.Must(template.ParseFS(views.HTMLFiles, "*.html")),
	}
}

func (t *Template) Render(w io.Writer, name string, data interface{}, c echo.Context) error {
	return t.tmpl.ExecuteTemplate(w, name, data)
}
