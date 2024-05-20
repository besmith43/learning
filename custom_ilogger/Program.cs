using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using custom_ilogger;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true)
    .Build();

var services = new ServiceCollection();

services.AddSingleton(configuration);
services.AddLogging(lb => lb.AddConsoleLogger());

using var sp = services.BuildServiceProvider();
var logger = sp.GetRequiredService<ILogger<Program>>();
logger.LogTrace("Test");
logger.LogDebug("Test");
logger.LogInformation("Test");
logger.LogWarning("Test");
logger.LogError("Test");
logger.LogCritical("Test");
