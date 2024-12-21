#!/usr/bin/env dotnet-script


#r "nuget: Cocona, 2.2.0"
#r "nuget: Microsoft.Extensions.Logging, 9.0.0"

#nullable enable

using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddTransient<MyService>();

var app = builder.Build();

app.AddCommand(
(
	[Option('n', Description = "name of the user")]string? name,
	[Option('a', Description = "Description for an option?")]int? age,
	[Argument]string[] items,
	MyService myService
)
=>
{
	// Console.WriteLine($"{name} - {age}");
	Console.WriteLine("Hello World!");

	if (name is not null)
	{
		Console.WriteLine(name);
	}

	if (age is not null)
	{
		Console.WriteLine(age);
	}

	foreach (var item in items)
	{
		Console.WriteLine(item);
	}

	myService.Hello("Hello Konnichiwa!");
});

app.Run();


class MyService
{
    private readonly ILogger _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void Hello(string message)
    {
        _logger.LogInformation(message);
    }
}


