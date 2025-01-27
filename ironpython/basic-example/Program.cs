// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World from Dotnet!");


var eng = IronPython.Hosting.Python.CreateEngine();
var scope = eng.CreateScope();
scope.SetVariable("whoKnows", "passed param");
eng.ExecuteFile("./hello.py", scope);

