package views

import (
	"strconv"
	"time"

	"github.com/labstack/echo/v4"
	"theprimeagen.tv/htmx/internal/models"
)

var data = models.NewData()
var id = 3

func IndexHandler(c echo.Context) error {
	return c.Render(200, "index.html", models.NewPageData(*data, models.NewFormData()))
}

func PostContactFormHandler(c echo.Context) error {
	name := c.FormValue("name")
	email := c.FormValue("email")

	if models.ContactExists(data.Contacts, email) {
		formData := models.FormData{
			Errors: map[string]string{
				"email": "Email already exists",
			},
			Values: map[string]string{
				"name":  name,
				"email": email,
			},
		}

		return c.Render(422, "contact-form", formData)
	}

	contact := models.NewContact(id, name, email)
	id++
	data.Contacts = append(data.Contacts, contact)

	formData := models.NewFormData()
	err := c.Render(200, "contact-form", formData)

	if err != nil {
		return err
	}

	return c.Render(200, "oob-contact", contact)
}

func DeleteContactHandler(c echo.Context) error {
	idStr := c.Param("id")
	id, err := strconv.Atoi(idStr)

	if err != nil {
		return c.String(400, "Id must be an integer")
	}

	deleted := false
	for i, contact := range data.Contacts {
		if contact.Id == id {
			data.Contacts = append(data.Contacts[:i], data.Contacts[i+1:]...)
			deleted = true
			break
		}
	}

	if !deleted {
		return c.String(400, "Contact not found")
	}

	time.Sleep(1 * time.Second)

	return c.NoContent(200)
}
