
// clear does exactly what you think it does
// Console.Clear();

Console.WriteLine("".PadLeft(30, '='));

Console.WriteLine("C# program starts here");

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

Console.WriteLine("{0}{1}", "".PadLeft(5, ' '), "left path 5 spaces");

// no idea why this doesn't work and lines 35 & 37 do...
// Console.WriteLine("{0,5} {1}", "left path 5 spaces", "second thing");

Console.Error.WriteLine("This should go to standard error");


const string format = "{0,-32} :{1}";

Console.WriteLine(format, "Key", "Value");

