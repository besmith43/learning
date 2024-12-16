#!/usr/bin/env dotnet-script

#r "nuget: Microsoft.Data.Sqlite, 9.0.0"


/*
 * NOTE: there is no SqliteDataReader that allows you to get a DataSet in the Microsoft.Data.Sqlite
 *		unfortunately, the System.Data.Sqlite that does have this is basically deprecated
 */



using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;


if (!File.Exists("./people.sqlite"))
{
	var create_table = @"CREATE TABLE contacts (
	contact_id INTEGER PRIMARY KEY,
	first_name TEXT NOT NULL,
	last_name TEXT NOT NULL,
	email TEXT NOT NULL UNIQUE,
	phone TEXT NOT NULL UNIQUE
)";

	Console.WriteLine("Seeding DB");

	Console.WriteLine($"Table Creation Effect: {NonQuery(create_table)}");

	var insertContacts = @"INSERT INTO contacts (contact_id, first_name, last_name, email, phone) VALUES (1, 'John', 'Doe', 'johndoe@gmail.com', '999-333-4444')";

	Console.WriteLine($"Insert Person 1: {NonQuery(insertContacts)}");

	insertContacts = @"INSERT INTO contacts (contact_id, first_name, last_name, email, phone) VALUES (2, 'Jane', 'Doe', 'janedoe@gmail.com', '999-333-4445')";

	Console.WriteLine($"Insert Person 2: {NonQuery(insertContacts)}");

	insertContacts = @"INSERT INTO contacts (contact_id, first_name, last_name, email, phone) VALUES (3, 'Jim', 'Smith', 'jimsmith@gmail.com', '999-333-4446')";

	Console.WriteLine($"Insert Person 3: {NonQuery(insertContacts)}");
}


var query = @"select * from contacts";


var reader = SelectContactsQuery(query);


while (reader.Read())
{
	Console.WriteLine($"Contact Entry: {reader.GetString(1)} {reader.GetString(2)}");
}


reader = QueryByLastName("Smith");

while (reader.Read())
{
	Console.WriteLine($"All Smiths: {reader.GetString(1)} {reader.GetString(2)}");
}




SqliteDataReader SelectContactsQuery(string sqlStatement)
{
	var connection = new SqliteConnection("Data Source=people.sqlite");
	connection.Open();

	var command = connection.CreateCommand();
	command.CommandText = sqlStatement;

	return command.ExecuteReader();
}


SqliteDataReader QueryByLastName(string lastName)
{
	var connection = new SqliteConnection("Data Source=people.sqlite");
	connection.Open();

	var command = connection.CreateCommand();
	command.CommandText = "select * from contacts where last_name = @lastName";
	command.Parameters.AddWithValue("lastName", lastName);

	return command.ExecuteReader();
}


int NonQuery(string sqlStatement)
{
	var connection = new SqliteConnection("Data Source=people.sqlite");
	connection.Open();

	var command = connection.CreateCommand();
	command.CommandText = sqlStatement;

	return command.ExecuteNonQuery();
}




