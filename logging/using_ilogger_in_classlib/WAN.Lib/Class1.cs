using Microsoft.Extensions.Logging;

namespace WAN.Lib;

public class Class1
{
	private readonly ILogger<Class1> logger;
	public Class1(ILogger<Class1> log = null)
	{
		logger = log;
	}

	public void DoWork()
	{
		// trace and debug isn't showing up?
		logger?.LogTrace("This is a debug message from DoWork function");
		logger?.LogDebug("This is a debug message from DoWork function");
		logger?.LogInformation("This is a debug message from DoWork function");
		logger?.LogWarning("This is a debug message from DoWork function");
		logger?.LogError("This is a debug message from DoWork function");
		logger?.LogCritical("This is a debug message from DoWork function");
		Console.WriteLine("Doing some Work");
	}

	public int Add(int a, int b)
	{
		logger?.LogInformation($"Parameter A: {a}");
		logger?.LogInformation($"Parameter B: {b}");

		var result = a + b;

		logger?.LogDebug($"Result: {result}");
		return result;
	}
}
