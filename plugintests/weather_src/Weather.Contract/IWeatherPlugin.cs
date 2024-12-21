using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.Contract
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public string Origin { get; set; }
    }

    public interface IWeatherPlugin
    {
        Task<WeatherForecast> GetWeatherForecast();
    }
}
