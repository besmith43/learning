
Console.WriteLine("Input Redirected: " + Console.IsInputRedirected);


foreach (var arg in args)
{
	Console.WriteLine("Arg: " + arg);
}

// if there is something being piped into the executable,
// isInputRedirected is true
if (Console.IsInputRedirected)
{
	using (var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
	{
		var input = sr.ReadToEnd();
		var tokens = input.Replace(Environment.NewLine, " ").Split(' ');
		Console.WriteLine($"Tokens: {tokens.Count()}");
		Console.WriteLine(input);
	}
}

Console.WriteLine("This should go to standard out");

Console.Error.WriteLine("This should go to standard error");


