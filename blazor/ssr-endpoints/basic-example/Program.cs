using System.Net;
using basic_example.Components;

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

app.MapGet("/api/test", (HttpRequest request, HttpResponse response) => {
    var accept = request.Headers.Accept;
    if (String.IsNullOrEmpty(accept))
    {
        Console.WriteLine($"Accept: null");
    }
    else
    {
        Console.WriteLine($"Accept: {accept}");
    }

    if (accept == "text/html")
    {
        return "<h4>Hello User</h4>";
    }
    else if (accept == "application/json")
    {
        return "{\"Message\":\"Hello User\"}";
    }
    else
    {
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        return "Accept type should be text/html or application/json";
    }
});


app.MapGet("/api/cookietest", (HttpRequest request, HttpResponse response) => {
    var cookies = request.Cookies;

    if (cookies.Count > 0)
    {
        foreach (var cookie in cookies)
        {
            Console.WriteLine($"Cookie Key: {cookie.Key}");
            Console.WriteLine($"Cookie Value: {cookie.Value}");
        }
    }
    else
    {
        Console.WriteLine("You have no cookies");
    }

    return "Hello Cookie Test User";
});


app.MapPost("/api/samltest", (HttpRequest request, HttpResponse response) => {



    return "Hello SAML Test User";
});


app.Run();
