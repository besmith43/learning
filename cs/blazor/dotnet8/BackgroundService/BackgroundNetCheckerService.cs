using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// see this link for where I got this idea from
// https://khalidabuhakmeh.com/access-background-services-from-aspnet-core


namespace dotnet8.BackgroundService;
public class BackgroundNetCheckerService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<BackgroundNetCheckerService> _logger;
    private Timer? _timer = null;

    public BackgroundNetCheckerService(ILogger<BackgroundNetCheckerService> logger)
    {
        _logger = logger;
    }

    public int GetExecutionCount()
    {
        return executionCount;
    }

    // only runs once when the service starts
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    // gets run every 5 seconds
    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    // only runs once when the service stops
    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    // only runs once when the service is disposed
    // think of it like you would a deconstructor
    public void Dispose()
    {
        _logger.LogInformation("Timed Hosted Service is disposing.");
        _timer?.Dispose();
    }
}