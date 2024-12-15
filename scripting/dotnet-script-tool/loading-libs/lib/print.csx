#!/usr/bin/env dotnet-script

if (Environment.GetCommandLineArgs()[1].Contains("print.csx"))
{
	var env = "TEST";
	print(env);
}


void print(string env)
{
	Console.WriteLine($"ENV: {env}");
}



