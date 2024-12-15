# loading libs

### lessons learned

1) dotnet script is defaults to using the top-level statement rules

2) the env variable is be default a global variable and can be used by the print function

3) you don't have to use static for shared functions that get loaded in dynamically

4) when you publish a script as a single file executable (default publish behavior), it'll pull in those loaded scripts automatically

5) the load paths are always relative to the script that's loading it.  Not based on where you call it from

6) all C# code must come after the #r and #load statements to load in nuget packages and csx files

7) you can run the "lib" scripts with a check for the script file's name like this
	- this also means that you should really treat the lib script's functions like static functions


```csharp

if (Environment.GetCommandLineArgs()[1].Contains("print.csx"))
{
	// treat this like main
}
```
