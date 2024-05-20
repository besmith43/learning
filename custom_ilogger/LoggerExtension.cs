using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace custom_ilogger;

public static class LoggerExtension
{
	public static ILoggingBuilder AddConsoleLogger(
        this ILoggingBuilder builder)
    {
		builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <LoggerConfig, LoggerProvider>(builder.Services);

        return builder;
	}
}