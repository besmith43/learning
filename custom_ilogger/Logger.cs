using Microsoft.Extensions.Logging;

namespace custom_ilogger;

public sealed class Logger(
    string name,
    Func<LoggerConfig> getCurrentConfig
) : ILogger
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel)
    {
		if (getCurrentConfig().DefaultLogLevel >= (int)logLevel)
		{
			return true;
		}
		else
		{
			return false;
		}
    }

    public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter
	)
    {
		if (!IsEnabled(logLevel))
		{
			return;
		}

		Console.WriteLine($"{logLevel}: {name} - {formatter(state, exception)}");

/*
		switch (logLevel)
		{
			case LogLevel.Critical:
				Console.WriteLine("Critical");
				break;

			case LogLevel.Error:
				Console.WriteLine("Error");
				break;

			case LogLevel.Debug:
				Console.WriteLine("Debug");
				break;

			case LogLevel.Warning:
				Console.WriteLine("Warning");
				break;

			case LogLevel.Trace:
				Console.WriteLine("Trace");
				break;

			case LogLevel.Information:
			default:
				Console.WriteLine("Information");
				break;
		}*/
    }
}