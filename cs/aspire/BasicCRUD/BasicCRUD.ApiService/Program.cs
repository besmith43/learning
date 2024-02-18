using BasicCRUD.ApiService.DataStore;
using BasicCRUD.Shared.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BasicCRUD.ApiService", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<DataStoreFactory>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// var locations = new[]
// {
//     "Seattle", "New York", "London", "Tokyo", "Paris", "Los Angeles", "Sydney", "Melbourne", "Auckland", "Wellington"
// };

// List<WeatherForecast> forecasts = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         {
//             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             TemperatureC = Random.Shared.Next(-20, 55),
//             Summary = summaries[Random.Shared.Next(summaries.Length)],
//             Location = locations[Random.Shared.Next(locations.Length)]
//         })
//         .ToList<WeatherForecast>();




// app.MapGet("/weatherforecast", () =>
// {
//     return forecasts;
// });


// app.MapGet("/weatherforecast/{city}", (string city) =>
// {
//     return forecasts.Where(x => x.Location == city);
// });


// app.MapPost("/weatherforecast", ([FromBody]WeatherForecast forecast) =>
// {
//     forecasts.Add(forecast);
//     return forecast;
// });

// app.MapPut("/weatherforecast", ([FromBody]WeatherForecast forecast) =>
// {
//     var existing = forecasts.FirstOrDefault(x => x.Date == forecast.Date && x.Location == forecast.Location);
//     if (existing != null)
//     {
//         forecasts.Remove(existing);
//         forecasts.Add(forecast);
//     }
//     return forecast;
// });

// app.MapDelete("/weatherforecast/{date}", ([FromBody]WeatherForecast forecast) =>
// {
//     var existing = forecasts.FirstOrDefault(x => x.Date == forecast.Date && x.Location == forecast.Location);
//     if (existing != null)
//     {
//         forecasts.Remove(existing);
//     }
//     return existing;
// });


app.MapDefaultEndpoints();


app.MapControllers();

app.Run();

