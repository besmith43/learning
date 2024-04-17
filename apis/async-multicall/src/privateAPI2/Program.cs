using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/building/{BuildingID}", GetBuilding);

app.Run();

IResult GetBuilding(int BuildingID)
{
    return Results.Ok("Here is your building data!");
}