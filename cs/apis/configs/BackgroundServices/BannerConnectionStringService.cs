using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using configs.utils;

namespace configs.BackgroundServices;

public class BannerConnectionStringService : IHostedService, IDisposable
{
    public string ConnectionString { get; set; } = string.Empty;
    private readonly ILogger<BannerConnectionStringService> _logger;
    private Timer? _timer = null;

    public BannerConnectionStringService(ILogger<BannerConnectionStringService> logger)
    {
        _logger = logger;
    }


    // only runs once when the service starts
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(GetBannerConnectionString, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(10));

        return Task.CompletedTask;
    }

    private void GetBannerConnectionString(object? state)
    {
        _logger.LogInformation("Getting Banner Connection String");
        ConnectionString = "Banner Connection String" + DateTime.Now.ToString();
        SharedData.ConnectionString = ConnectionString;
        _logger.LogInformation("Banner Connection String Updated");
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