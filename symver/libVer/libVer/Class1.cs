namespace libVer;

public static class Class1
{
	public static void PrintVersion()
	{
		Console.WriteLine($"Program Version: {typeof(Class1).Assembly.GetName().Version}");
	}
}
