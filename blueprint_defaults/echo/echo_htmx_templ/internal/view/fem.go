package view

import (
	"echo_htmx_templ/internal/database"
	"strconv"
	"time"

	"github.com/labstack/echo/v4"
)

var data = database.NewData()
var id = 3

func IndexHandler(c echo.Context) error {
	return render(c, 200, FEM(database.NewPageData(*data, database.NewFormData())))
}

func PostContactFormHandler(c echo.Context) error {
	name := c.FormValue("name")
	email := c.FormValue("email")

	if database.ContactExists(data.Contacts, email) {
		formData := database.FormData{
			Errors: map[string]string{
				"email": "Email already exists",
			},
			Values: map[string]string{
				"name":  name,
				"email": email,
			},
		}

		return render(c, 422, ContactForm(formData))
	}

	contact := database.NewContact(id, name, email)
	id++
	data.Contacts = append(data.Contacts, contact)

	formData := database.NewFormData()
	err := render(c, 200, ContactForm(formData))

	if err != nil {
		return err
	}

	return render(c, 200, OobContact(contact))
}

func DeleteContactHandler(c echo.Context) error {
	idStr := c.Param("id")
	id, err := strconv.Atoi(idStr)

	if err != nil {
		return c.String(400, "Id must be an integer")
	}

	deleted := false
	for i, contact := range data.Contacts {
		if contact.ID == id {
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
