using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITemplate.Helpers;
using WebAPITemplate.Models;
using DataStore;

namespace WebAPITemplate.BackgroundServices;

public class BannerConnectionStringService : IHostedService, IDisposable
{
    public string ConnectionString { get; set; } = string.Empty;
    private readonly ILogger<BannerConnectionStringService> _logger;
    private readonly IConfiguration _config;
    private Timer? _timer = null;

    public BannerConnectionStringService(ILogger<BannerConnectionStringService> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }


    // only runs once when the service starts
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(GetBannerConnectionString, null, TimeSpan.Zero,
            TimeSpan.FromHours(6));

        return Task.CompletedTask;
    }

    private void GetBannerConnectionString(object? state)
    {
        string strQuery = "SELECT BannerHost, BannerPort, BannerServiceName, BannerUsername, BannerPassword FROM express.tblLocalizations WHERE Instance = @Instance";

        var dataStore = new MSSQL<BannerConnection>();

        dataStore.ConnectionString = SharedData.ExpressDBConnectionString;

        dataStore.AddParameter("@Instance", SharedData.Instance);

        var result = dataStore.RunGet(strQuery);

        if (result.IsT1) // is error check
        {
            _logger.LogError("Error: get Banner Connection string - " + result.AsT1.Message);
            return;
        }
        else
        {
            var bannerConnection = result.AsT0[0];
            return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + bannerConnection.BannerHost + ")(PORT=" + bannerConnection.BannerPort + "))(CONNECT_DATA=(Server=DEDICATED)(SERVICE_NAME = " + bannerConnection.BannerServiceName + "))); User Id = " + bannerConnection.BannerUsername + "; Password = " + bannerConnection.BannerPassword + ";";
        }
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