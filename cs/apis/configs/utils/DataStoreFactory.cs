using configs.Models;
using Microsoft.AspNetCore.SignalR;

namespace configs.utils;

public class DataStoreFactory
{
    private readonly ILogger<DataStoreFactory> _logger;
	private readonly IConfiguration _config;

	public DataStoreFactory(ILogger<DataStoreFactory> logger, IConfiguration config)
	{
		_config = config;
		_logger = logger;
	}


    public DataStore GetDataStore()
    {
        return new DataStore
        {
            ConnectionString = SharedData.ConnectionString,
            Instance = _config["Instance"]
        };
    }
}