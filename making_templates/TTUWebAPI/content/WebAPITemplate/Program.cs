



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var location = new Uri(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase);
var currentDirectory = new FileInfo(location.AbsolutePath).Directory.FullName;

var appSettingsFilePath = Path.Combine(currentDirectory, "appsettings.json");

builder.Configuration.AddJsonFile(appSettingsFilePath, optional: false, reloadOnChange: true);

builder.Services.AddHealthChecks()
    .AddCheck<ExpressDBHealthCheck>("ExpressDBHealthCheck", tags: new[] { "ExpressDBHealthCheck" })
    .AddCheck<BannerDBHealthCheck>("BannerDBHealthCheck", tags: new[] { "BannerDBHealthCheck" })
    .AddCheck<ADHealthCheck>("ADHealthCheck", tags: new[] { "ADHealthCheck" });

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

builder.Services.AddHostedService<BannerConnectionStringService>();

var app = builder.Build();

app.UseCors("Cors");

AppSettingsHelper.AppSettingsConfigure(app.Services.GetRequiredService<IConfiguration>());

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
#if DEBUG
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPITemplate API V1");
#else
    // this is the uri path for use with IIS
    c.SwaggerEndpoint("/WebAPITemplate/swagger/v1/swagger.json", "WebAPITemplate API V1");
#endif
});

app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UserAuthorization();

app.MapControllers();

app.MapGet("/_serviceInfo/", GetServiceInfo);

app.Run();




[SwaggerOperation(Summary = "Get Service Info", Description = "This endpoint returns information about the runtime environment.", OperationId = "GetServiceInfo")]
[ProducesResponseType(typeof(ServiceInfo), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
IResult GetServiceInfo(HttpRequest request, HttpResponse response, IConfiguration config)
{
    return Results.Ok(new ServiceInfo());
}


