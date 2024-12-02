#!/usr/bin/env dotnet-script
#r "nuget: OneOf, 3.0.271"

using OneOf;


Console.WriteLine("Hello world!");

var result = getStuff();

result.Switch(
	stringVal => Console.WriteLine($"getStuff Result was a string with a value of : {stringVal}"),
	intVal => Console.WriteLine($"getStuff Result was a int with a value of : {intVal}")
);

OneOf<string, int> getStuff() {
	return "always blue";
}

