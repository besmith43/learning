using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using classlib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});



var app = builder.Build();

app.UseCors("Cors");

app.MapGet("/", () => "Hello World");
app.MapPost("/Login", Login);
app.MapPost("/Logout", Logout);
app.MapGet("/Data", GetData);

app.Run();

IResult Login()
{
    var authURL = Environment.GetEnvironmentVariable("auth");
    Console.WriteLine($"Auth URL: {authURL}");

    return Results.Ok("Hello World!");
}

IResult Logout()
{
    var authURL = Environment.GetEnvironmentVariable("auth");
    Console.WriteLine($"Auth URL: {authURL}");

    return Results.Ok("Goodbye World!");
}

async Task<IResult> GetData()
{
    var client = new HttpClient();

    var personAPIURL = Environment.GetEnvironmentVariable("api1");
    Console.WriteLine($"Person API Url: {personAPIURL}");
    var buildingAPIURL = Environment.GetEnvironmentVariable("api2");
    Console.WriteLine($"Building API Url: {buildingAPIURL}");
    var jobAPIURL = Environment.GetEnvironmentVariable("api3");
    Console.WriteLine($"Job API Url: {jobAPIURL}");

    // call person api
    var personTask = client.GetAsync($"{personAPIURL}/person/1");

    // call building api
    var buildingTask = client.GetAsync($"{buildingAPIURL}/building/1");

    // call job api
    var jobTask = client.GetAsync($"{jobAPIURL}/job/1");

    var personResponse = await personTask;
    var buildingResponse = await buildingTask;
    var jobResponse = await jobTask;

    string personResponseBody = await personResponse.Content.ReadAsStringAsync();
    personResponseBody = JsonSerializer.Deserialize<string>(personResponseBody)!;
    string buildingResponseBody = await buildingResponse.Content.ReadAsStringAsync();
    buildingResponseBody = JsonSerializer.Deserialize<string>(buildingResponseBody)!;
    string jobResponseBody = await jobResponse.Content.ReadAsStringAsync();
    jobResponseBody = JsonSerializer.Deserialize<string>(jobResponseBody)!;

    var combinedObject = new Combined {
        PersonName = personResponseBody,
        BuildingName = buildingResponseBody,
        JobTitle = jobResponseBody
    };

    // return Results.Ok($"{personResponseBody}  {buildingResponseBody}  {jobResponseBody}");
    return Results.Ok(combinedObject);
}