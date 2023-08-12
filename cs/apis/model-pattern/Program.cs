using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc;
using model_pattern.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    }
);

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/widget"))
    {
        var test_thingy = "testing";
        context.Items.Add("test_thingy", test_thingy);

        if (context.Request.Headers.ContainsKey("SessionID"))
        {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Don't need a session id");
                return;
        }
    }

    // Do work that can write to the Response.
    await next(context);
    // Do logging or other work that doesn't write to the Response.
    Console.WriteLine("Request: " + context.Request.Path);
});

app.UseSwagger();
app.UseSwaggerUI(
    c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    }
);

app.MapPost("/widget/", CreateWidget)
.WithName("CreateWidget")
.WithOpenApi();

app.MapGet("/hello", HelloWorld)
.WithName("HelloWorld")
.WithOpenApi();

app.MapGet("/bad", BadWorld)
.WithName("BadWorld")
.WithOpenApi();

app.MapGet("/hello2", HelloWorld)
.WithName("HelloWorld2")
.WithOpenApi();

app.MapGet("/bad2", BadWorld2)
.WithName("BadWorld2")
.WithOpenApi();

app.MapGet("/actions", ActionTest)
.WithName("ActionTest")
.WithOpenApi();

app.MapGet("/actions2", ActionTest2)
.WithName("ActionTest2")
.WithOpenApi();

app.MapGet("/authors/{id}", async ([FromServices] Author entity, int id) =>
{
    return Results.Ok();
});
app.MapPost("/authors", async ([FromServices] Author entity) =>
{
    return Results.Ok();
});
app.MapPut("/authors/{id}", async ([FromServices] int id, Author entityToUpdate) =>
{
    return Results.Ok();
});
app.MapDelete("/authors/{id}", async ([FromServices] int id) =>
{
    return Results.Ok();
});

app.Run();

[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
static Results<Ok<string>, BadRequest> HelloWorld()
{
    return TypedResults.Ok("Hello World!");
}

static Results<Ok<string>, BadRequest> HelloWorld2()
{
    return TypedResults.Ok("Hello World!");
}

[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
static Results<Ok<string>, BadRequest> BadWorld()
{
    return TypedResults.BadRequest();
}

static Results<Ok<string>, BadRequest> BadWorld2()
{
    return TypedResults.BadRequest();
}

[SwaggerOperation(Summary = "Test action", Description = "Test action description", OperationId = "ActionTest", Tags = new[] { "Test" })]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(typeof(Widget), StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
static IResult ActionTest(string action)
{
    switch (action)
    {
        case "ok":
            return Results.Ok("Successfully executed action");
        case "bad":
            var thingy = new Widget
            {
                Name = "Thingy",
                Id = 1,
                Status = WidgetStatus.Active
            };
            return Results.BadRequest(thingy);
        case "notfound":
            return Results.NotFound();
        case "forbidden":
            return Results.Forbid();
        case "unauthorized":
            return Results.Unauthorized();
        case "internalservererror":
            return Results.StatusCode(500);
        default:
            return Results.Ok<string>($"Action: {action}");
    }
}

static IActionResult ActionTest2(string action)
{
    switch (action)
    {
        case "ok":
            return new Microsoft.AspNetCore.Mvc.OkResult();
        case "bad":
            return new Microsoft.AspNetCore.Mvc.BadRequestResult();
        case "notfound":
            return new Microsoft.AspNetCore.Mvc.NotFoundResult();
        case "forbidden":
            return new Microsoft.AspNetCore.Mvc.ForbidResult();
        case "unauthorized":
            return new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        case "internalservererror":
            return new Microsoft.AspNetCore.Mvc.StatusCodeResult(500);
        default:
            return new Microsoft.AspNetCore.Mvc.OkObjectResult($"Action: {action}");
    }
}

[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
static IResult CreateWidget([FromBody] Widget newWidget, HttpContext context)
{
    if (context.Items.TryGetValue("test_thingy", out var test_thingy))
    {
        Console.WriteLine(test_thingy);
    }

    return Results.Ok("Success");
}

class Widget
{
    [SwaggerSchema("The name of the widget")]
    public string Name { get; set; }
    public int Id { get; set; }
    public string Description { get; set; }
    public WidgetStatus Status { get; set; }
}

// [JsonConverter(typeof(StringEnumConverter))] // for newtonsoft
[JsonConverter(typeof(JsonStringEnumConverter))] // for system.text.json
enum WidgetStatus
{
    Active,
    Inactive
}
