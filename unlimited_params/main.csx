#!/usr/bin/env dotnet-script


print("this is a test", "I can have how many params?", "unlimited input!!!!");



print("1", "2", "3", "4", "5");


void print(params string[] list)
{
	foreach (var item in list)
	{
		Console.WriteLine(item);
	}
}


