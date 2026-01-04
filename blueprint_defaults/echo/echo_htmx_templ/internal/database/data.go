package database

type Contact struct {
	Name  string
	Email string
	ID    int
}

type ContactList struct {
	Contacts []Contact
}

func NewData() *ContactList {
	return &ContactList{
		Contacts: []Contact{
			{
				Name:  "John Doe",
				Email: "john.doe@gmail.com",
				ID:    1,
			},
			{
				Name:  "Jane Doe",
				Email: "jain.doe@gmail.com",
				ID:    2,
			},
		},
	}
}

type FormData struct {
	Errors map[string]string
	Values map[string]string
}

func NewFormData() FormData {
	return FormData{
		Errors: map[string]string{},
		Values: map[string]string{},
	}
}

type PageData struct {
	Data ContactList
	Form FormData
}

func NewContact(id int, name, email string) Contact {
	return Contact{
		ID:    id,
		Name:  name,
		Email: email,
	}
}

func NewPageData(data ContactList, form FormData) PageData {
	return PageData{
		Data: data,
		Form: form,
	}
}

func ContactExists(contacts []Contact, email string) bool {
	for _, c := range contacts {
		if c.Email == email {
			return true
		}
	}
	return false
}
