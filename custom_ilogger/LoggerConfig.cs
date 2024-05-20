using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace custom_ilogger;

public sealed class LoggerConfig
{
	public string Default { get; set; }
	public int DefaultLogLevel { get; set; }

	public void UpdateDefault()
	{
		// this console writeline works... however...
		// it doesn't print anything for the value of default
		// everything else after it though is fine.. so idk
		// Console.WriteLine($"Default Log Level is {Default}");

		switch(Default)
		{
			case "Trace":
				DefaultLogLevel = 0;
				break;
			case "Debug":
				DefaultLogLevel = 1;
				break;
			case "Information":
				DefaultLogLevel = 2;
				break;
			case "Warning":
				DefaultLogLevel = 3;
				break;
			case "Error":
				DefaultLogLevel = 4;
				break;
			case "Critical":
				DefaultLogLevel = 5;
				break;
			case "None":
			default:
				DefaultLogLevel = 6;
				break;
		}
	}
}