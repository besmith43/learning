using CRUDShared.Models;

namespace CRUDSSR.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync()
    {
        Console.WriteLine("Calling Weather API");
        return await httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast") ?? [];
    }
}