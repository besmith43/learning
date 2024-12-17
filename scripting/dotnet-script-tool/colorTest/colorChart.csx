#!/usr/bin/env dotnet-script

// this nested for loop is from wikipedia
// https://en.wikipedia.org/wiki/ANSI_escape_code

for (int i = 0; i < 11; i++)
{
	for (int j = 0; j < 10; j++)
	{
		int n = 10 * i + j;

		if (n > 108)
		{
			break;
		}

		// original C statement
		// printf("\033[%dm %3d\033[m", n, n);
		// Console.Write("\033[{0}m {1}\033[m", n, n);
		Console.Write("\x1b[{0}m {1,3}\x1b[m", n, n);
	}
	Console.WriteLine();
}



