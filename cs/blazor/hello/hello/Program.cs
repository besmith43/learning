using hello.Client.Pages;
using hello.Components;
using hello.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => 
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient();

var app = builder.Build();

app.UsePathBase("/Hello");

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(hello.Client._Imports).Assembly);

app.MapGet("/api/world", () => "{ \"message\": \"Hello World!\" }");

int currentCount = 0;

app.MapGet("/api/counter", GetCounter);

app.Run();


Results<Ok<hello.Shared.Models.Counter>, BadRequest<ErrorMessage>> GetCounter()
{
    currentCount++;

    if (currentCount % 3 == 0)
    {
        return TypedResults.BadRequest( new ErrorMessage { Message = "Oops! Something went wrong." } );
    }
    else
    {
        return TypedResults.Ok( new hello.Shared.Models.Counter { Count = currentCount } );
    }
}