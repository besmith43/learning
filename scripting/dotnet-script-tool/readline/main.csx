#!/usr/bin/env dotnet-script


#r "nuget: ReadLine, 2.0.1"


string input = System.ReadLine.Read("(prompt)> ");

Console.WriteLine(input);

/*
 * getting this error when attempting to run
 * main.csx(7,16): error CS0119: 'Console.ReadLine()' is a method, which is not valid in the given context
 * it also isn't allowing me to publish the main.csx script
 *
 * SOLUTION:
 * you have to call it's full namespace path
 * System.ReadLine.Read
 */
