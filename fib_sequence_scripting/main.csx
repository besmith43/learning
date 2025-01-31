#!/usr/bin/env dotnet-script


if (Args.Count != 1)
{
    Console.Error.WriteLine("you need to pass in a number");
    return;
}

int x = Int32.Parse(Args[0]);

var answer = calculateFib(x);

Console.WriteLine(answer);

int calculateFib(int num)
{
	if (num == 1)
	{
		return 0;
	}

	if (num == 2)
	{
		return 1;
	}

	var x = 1;
	var y = 1;
	var sum = 0;

	for (int i = 2; i < num; i++)
	{
		sum = x + y;
		x = y;
		y = sum;	
	}

	return sum;
}



