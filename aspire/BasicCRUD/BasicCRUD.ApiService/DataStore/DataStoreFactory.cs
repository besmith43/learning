using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicCRUD.Shared.Models;
using OneOf;

namespace BasicCRUD.ApiService.DataStore;

public class DataStoreFactory
{
    private string[] summaries;
    private string[] locations;
    private List<WeatherForecast> forecasts;
    private int CallCount = 0;

    public DataStoreFactory()
    {
        summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        locations = new[]
        {
            "Seattle", "New York", "London", "Tokyo", "Paris", "Los Angeles", "Sydney", "Melbourne", "Auckland", "Wellington"
        };

        forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)],
                    Location = locations[Random.Shared.Next(locations.Length)]
                })
                .ToList<WeatherForecast>();
    }

    public OneOf<WeatherForecast, ErrorMessage> AddWeatherForecast(WeatherForecast forecast)
    {
        if (IsError())
        {
            Console.WriteLine("AddWeatherForecast error");
            return GetErrorMessage();
        }

        Console.WriteLine("AddWeatherForecast success");
        forecasts.Add(forecast);
        return forecast;
    }

    public OneOf<List<WeatherForecast>, ErrorMessage> GetAllWeatherForecasts()
    {
        if (IsError())
        {
            Console.WriteLine("GetAllWeatherForecasts error");
            return GetErrorMessage();
        }

        Console.WriteLine("GetAllWeatherForecasts success");
        return forecasts;
    }

    public OneOf<List<WeatherForecast>, ErrorMessage> GetWeatherForecastByCity(string city)
    {
        if (IsError())
        {
            return GetErrorMessage();
        }

        return forecasts.Where(x => x.Location == city).ToList();
    }

    public OneOf<WeatherForecast, ErrorMessage> UpdateWeatherForecast(WeatherForecast forecast)
    {
        if (IsError())
        {
            return GetErrorMessage();
        }

        var existing = forecasts.FirstOrDefault(x => x.Date == forecast.Date && x.Location == forecast.Location);
        if (existing != null)
        {
            forecasts.Remove(existing);
            forecasts.Add(forecast);
        }
        return forecast;
    }

    public OneOf<WeatherForecast, ErrorMessage> DeleteWeatherForecast(WeatherForecast forecast)
    {
        if (IsError())
        {
            return GetErrorMessage();
        }

        var existing = forecasts.FirstOrDefault(x => x.Date == forecast.Date && x.Location == forecast.Location);
        if (existing != null)
        {
            forecasts.Remove(existing);
        }
        return forecast;
    }

    private bool IsError()
    {
        CallCount++;
        if (CallCount % 3 == 0)
        {
            return true;
        }
        return false;
    }

    private ErrorMessage GetErrorMessage()
    {
        return new ErrorMessage
        {
            Message = "An error occurred.",
            StackTrace = "This is a fake stack trace.",
            StatusCode = System.Net.HttpStatusCode.InternalServerError
        };
    }
}