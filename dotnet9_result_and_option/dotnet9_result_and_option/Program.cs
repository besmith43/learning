using LanguageExt;
using LanguageExt.Common;

Option<string> option = Environment.GetCommandLineArgs().FirstOrDefault();

option.Match(
    s =>
    {
        Console.WriteLine($"Option : {s}");
    },
    () =>
    {
        Console.WriteLine($"Option : {option}");
    });
    
    
    