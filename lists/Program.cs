using System;
using System.Collections;
using System.Collections.Generic;

namespace lists
{
    class Program
    {
        static void Main(string[] args)
        {
		var names = new List<string> { "<name>", "Ana", "Felipe" };
		foreach (var name in names)
		{
			  Console.WriteLine($"Hello {name.ToUpper()}!");
		}

		Console.WriteLine();
		names.Add("Maria");
		names.Add("Bill");
		names.Remove("Ana");
		foreach (var name in names)
		{
			  Console.WriteLine($"Hello {name.ToUpper()}!");
		}

		Console.WriteLine($"My name is {names[0]}.");
		Console.WriteLine($"I've added {names[2]} and {names[3]} to the list.");

		Console.WriteLine($"The list has {names.Count} people in it");

		var index = names.IndexOf("Felipe");
		if (index != -1)
			  Console.WriteLine($"The name {names[index]} is at index {index}");

		var notFound = names.IndexOf("Not Found");
		Console.WriteLine($"When an item is not found, IndexOf returns {notFound}");

		names.Sort();
		foreach (var name in names)
		{
			  Console.WriteLine($"Hello {name.ToUpper()}!");
		}

		var fibonacciNumbers = new List<int> {1, 1};

		var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
		var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

		fibonacciNumbers.Add(previous + previous2);

		foreach(var item in fibonacciNumbers)
			    Console.WriteLine(item);

		var fibonacciNumbers2 = new List<int> {1, 1};

		while (fibonacciNumbers2.Count < 20)
		{
			previous = fibonacciNumbers2[fibonacciNumbers2.Count - 1];
			previous2 = fibonacciNumbers2[fibonacciNumbers2.Count - 2];

			fibonacciNumbers2.Add(previous + previous2);
		}
		foreach(var item in fibonacciNumbers2)
			Console.WriteLine(item);
        }
    }
}
