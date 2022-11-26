using System;
using System.Data.SQLite;

// got this by following this tutorial
// https://zetcode.com/csharp/sqlite/

namespace sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = @"URI=file:.\test.db";

            using var con = new SQLiteConnection(cs);
            {
                con.Open();

                var cmd = new SQLiteCommand(con);

                cmd.CommandText = "DROP TABLE IF EXISTS cars";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
                                    name TEXT, price INT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
                cmd.ExecuteNonQuery();

                Console.WriteLine("Table cars created");
            }
        }
    }
}
