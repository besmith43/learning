using htmx_apikey_request.Components;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();


app.MapGet("/api", ([FromHeader]string SessionID) => {
    if (String.IsNullOrEmpty(SessionID))
    {
        return Results.BadRequest("Must have a Session ID");
    }

    return Results.Ok($"Hello {SessionID}!");
});

app.Run();
