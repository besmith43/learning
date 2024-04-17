using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/person/{PersonID}", GetPerson);

app.Run();

IResult GetPerson(int PersonID)
{
    return Results.Ok("Here is your person data!");
}