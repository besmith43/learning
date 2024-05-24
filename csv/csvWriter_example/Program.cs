using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using csvWriter_example.Models;


List<Widget> widgets = new List<Widget>();

widgets.Add(new Widget
{
	Name = "Chair",
	CreatedDate = new DateOnly(2023, 8, 15),
	Description = "A Wonderful Rocking Chair for Grandma",
	SerialNumber = "00000001",
	ExpirationDate = null,
	Price = new decimal(150)
});

widgets.Add(new Widget
{
	Name = "Dell XPS 13",
	CreatedDate = new DateOnly(2021, 3, 23),
	Description = "The best laptop you've ever seen",
	SerialNumber = "GADVWEB2",
	ExpirationDate = new DateOnly(2026, 3, 23),
	Price = new decimal(2499.99)
});


var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
	// Don't write the header again.
	// HasHeaderRecord = false,
	// Change the delimiter
	Delimiter = "|",
};

// this library will overwrite the output file unless the FileMode.append has been added to the StreamWriter
using (var writer = new StreamWriter("widgets.csv"))
// using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
using (var csv = new CsvWriter(writer, config))
{
	csv.WriteRecords(widgets);
}

