using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WAN.Lib;


// https://stackoverflow.com/questions/60688112/logging-in-console-application-net-core-with-di
// https://stackoverflow.com/questions/2764384/logging-in-a-c-sharp-library

/*
using var loggerFactory = LoggerFactory.Create(builder =>
		{
			builder
				.AddFilter("Microsoft", LogLevel.Trace)
				.AddFilter("System", LogLevel.Trace)
				.AddFilter("LoggingConsoleApp.Program", LogLevel.Trace)
				.AddConsole();
		});
ILogger logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Example log message");
*/

var services = new ServiceCollection();
ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();

var logger = serviceProvider.GetService<ILogger<Program>>();
var logger2 = serviceProvider.GetService<ILogger<Class1>>();
logger.LogInformation("Hello world!");

// https://betterstack.com/community/guides/logging/how-to-start-logging-with-net/
void ConfigureServices(ServiceCollection services)
{
	services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.ClearProviders();
                loggerBuilder.AddConsole();
                loggerBuilder.SetMinimumLevel(LogLevel.Trace);
            });
}


var thingy = new Class1(logger2);

thingy.DoWork();

Console.WriteLine($"8 + 3 = {thingy.Add(8, 3)}");
