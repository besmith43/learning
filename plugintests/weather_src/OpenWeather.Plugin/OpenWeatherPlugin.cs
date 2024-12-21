using Prise.Plugin;
using Weather.Contract;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace OpenWeather.Plugin;

[Plugin(PluginType = typeof(IWeatherPlugin))] // I didn't do this before
public class OpenWeatherPlugin : IWeatherPlugin
{
	public Task<WeatherForecast> GetWeatherForecast()
	{
		var rng = new Random();
		return Task.FromResult(new WeatherForecast
		{
			Date = DateTime.Now,
			TemperatureC = rng.Next(-20, 55),
			Summary = Summaries[rng.Next(Summaries.Length)],
			Origin = "OpenWeatherPlugin"
		});
	}

	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};
}
