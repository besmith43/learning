using Polly;
using Polly.Contrib.WaitAndRetry;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Local", client =>
    {
        client.BaseAddress = new Uri("https://localhost:7156");
    })
    // .AddPolicyHandler(Policy<HttpResponseMessage>
    //     .Handle<HttpRequestException>()
    //     .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
    //     .WaitAndRetryAsync(
    //         Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/", async (IHttpClientFactory clientFactory) => {
    var client = clientFactory.CreateClient("Local");
    var response = await client.GetAsync("/weatherforecast");
    var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    return forecast;
});

// TODO: test Polly with data access

int count = 0;

app.MapGet("/weatherforecast", async () =>
{
    count++;
    Console.WriteLine("Count: " + count);
    await Task.Delay(1000);

    if (count % 3 != 0)
    {
        throw new HttpRequestException("Something went wrong");
    }


    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
