using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WAN.Lib;

namespace WAN.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var thingy = new Class1();

        var result = thingy.Add(5, 3);

        Assert.Equal(8, result);
    }

    [Fact]
    public void Test2()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        var logger2 = serviceProvider.GetService<ILogger<Class1>>();

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

        var result = thingy.Add(5, 3);

        Assert.Equal(8, result);
    }
}