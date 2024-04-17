using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/job/{JobID}", GetJob);

app.Run();

IResult GetJob(int JobID)
{
    return Results.Ok("Here is your job data!");
}