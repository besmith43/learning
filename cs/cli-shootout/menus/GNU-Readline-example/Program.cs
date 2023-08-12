

ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

Console.WriteLine("Type 'git ' and press TAB to see the auto-completion in action between the following commands: init, clone, pull, & push!");
string input = ReadLine.Read("(prompt)> ");
Console.WriteLine(input);



class AutoCompletionHandler : IAutoCompleteHandler
{
    // characters to start completion from
    public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

    // text - The current text entered in the console
    // index - The index of the terminal cursor within {text}
    public string[] GetSuggestions(string text, int index)
    {
        if (text.StartsWith("git "))
            return new string[] { "init", "clone", "pull", "push" };
        else
            return null;
    }
}



