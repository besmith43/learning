using Cocona;
using Spectre;
using Spectre.Console;

CoconaApp.Run((string name, string color = "") =>
{
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

	Color messageColor;
	if (color == "green")
	{
		messageColor = Color.Green;
	}
	else
	{
		messageColor = Color.Red;
	}

	AnsiConsole.Write(new FigletText("Hello, " + name).LeftJustified().Color(messageColor));

	// throws an error that the terminal isn't interactive?
	// if there standard input is redirected into the app
	if (Console.IsInputRedirected)
	{
		var stdInput = Console.In;
		Console.WriteLine("Standard Input: " + stdInput);
		Console.WriteLine("Interactive Console: " + AnsiConsole.Profile.Capabilities.Interactive);

		Console.In.Close();
		var reader = new StreamReader(Console.OpenStandardInput());
		Console.SetIn(reader);

		Console.WriteLine("Interactive Console (after new stdin): " + AnsiConsole.Profile.Capabilities.Interactive);
		Console.WriteLine("Is Input still redirected? " + Console.IsInputRedirected);
		// Console.WriteLine("Are keys available: " + Console.KeyAvailable);
		Console.WriteLine("Console In Peek: " + Console.In.Peek());

		return;
	}
	else
	{
		var stdInput = Console.In;
		Console.WriteLine("Standard Input: " + stdInput.ToString());
		Console.WriteLine("Interactive Console: " + AnsiConsole.Profile.Capabilities.Interactive);
	}

	// Ask for the user's favorite fruits
	var fruits = AnsiConsole.Prompt(
		new MultiSelectionPrompt<string>()
			.Title("What are your [green]favorite fruits[/]?")
			.NotRequired() // Not required to have a favorite fruit
			.PageSize(10)
			.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
			.InstructionsText(
				"[grey](Press [blue]<space>[/] to toggle a fruit, " +
				"[green]<enter>[/] to accept)[/]")
			.AddChoices(new[] {
			"Apple", "Apricot", "Avocado",
			"Banana", "Blackcurrant", "Blueberry",
			"Cherry", "Cloudberry", "Coconut",
			}));

	// Write the selected fruits to the terminal
	foreach (string fruit in fruits)
	{
		AnsiConsole.WriteLine(fruit);
	}
});