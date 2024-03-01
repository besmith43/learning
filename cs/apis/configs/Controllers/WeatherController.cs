using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using configs.Models;
using configs.utils;

namespace configs.Controllers;

[ApiController]
// [Route("[controller]")]
public class WeatherController : ControllerBase
{
	private readonly ILogger<WeatherController> _logger;
	private readonly IConfiguration _config;
    private readonly DataStoreFactory _dataStore;

	public WeatherController(ILogger<WeatherController> logger, IConfiguration config, DataStoreFactory dataStore)
    {
        _config = config;
        _logger = logger;
        _dataStore = dataStore;
    }

	[Route("Weather")]
	[HttpGet]
	public IResult GetAllWeather()
	{
        return Results.Ok(_dataStore.GetDataStore());
    }
}
