using Microsoft.OpenApi.Models;
using api.Authentication;
using api.Models;
using basic_example.Components;
using api;


// SharedData.SamlInfo.SAMLApplicationID = "Nevermind";
SharedData.SamlInfo.SAMLApplicationID = "http://localhost:5095/";
SharedData.SamlInfo.SAMLConsumerURL = "http://localhost:5095/";
SharedData.SamlInfo.SAMLEndPoint = "https://login.microsoftonline.com/9ae265cb-9595-4dab-9318-79419ca56ecf/saml2";
SharedData.SamlInfo.SAMLCert = @"-----BEGIN CERTIFICATE----- MIIC8DCCAdigAwIBAgIQNQtca5lrHpdCjb9w3mHAjzANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQD EylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMzAzMjQxMzIz MTBaFw0yNjAzMjQxMzIzMDRaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQg U1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAywASTzRWJGez D6I8CpHhklw4KAQrvkByLhLSds+HW33Ba8+9kv2wTjxozyWSnJRqe69ha78EpuyGbeW37/yzIUvk 1zvfS7ifdS4OZRWCXcSdDnwgScdWgHUpKzOz33J9+zBNR6Uwaj2BLgmgk8VeRG8BicSXobV1wYot tTiCK+TwFzyoMKMa8ODcUsboglkxvTiwk2ZGE/GEaxgBEsQia5xVZodyGq2ZyX+g6iLhdT2Uie0V O1BKIa1A9kd8/zB6RuibITMc6wcoCRcXkNKpWn79HYDWGe5Jrd3JuhYh2KVKyHzKrMwWSqTBZKfv ZSNC2gMxqnmVrhpQgpdpKH7XuQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQC1lsatziB4imOAgyWO iOFsJ1xp8xcyzCsdjRUik9KN2+lgUrj6KfXgJbGjKXQY5X5PVGJGW2bO6zItpFFp4I8+FIB+FB41 R1xQupoCwDJlArGsmZb0Sx9cl8bn3O6YJcuGTfD8S1uTYe3z5opUpZva6cGQTfrhJRtDA7wcmxin a4Oc0kJwrc9RJcxsNF3m0mZMwR4jnrCBs7pSIT4Kqx36qqeKhBAaP+25LLe5yMYlA2m7JhG7Zo1p 1wOGNin+qjpmSl682QWjxAYKh47ILYtOILrH9YDLt9LV/XPClnUKpTuVVi32SUXA8JDQ6UY7PMHL 5d0xmTffuztIxexbNbBO -----END CERTIFICATE-----";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Nevermind", Version = "v1" });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.AddSecurityDefinition("APIKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "APIKey",
        Name = "APIKey",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "APIKey"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "APIKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };

    c.AddSecurityRequirement(requirement);
    c.EnableAnnotations();
});

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

builder.Services.AddScoped<AuthenticationAttribute>();

builder.Services.ConfigureHttpClientDefaults(http =>
{
    // Turn on resilience by default
    http.AddStandardResilienceHandler();

    // Turn on service discovery by default
    // http.UseServiceDiscovery();
});

builder.Services.AddRazorComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<PostFormService>();

builder.Services.AddAntiforgery(options => {
    // options.SuppressXFrameOptionsHeader = true;
    options.Cookie.Expiration = TimeSpan.Zero;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (SharedData.blnDebug)
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nevermind V1");
    }
    else
    {
        // this is the uri path for use with IIS
        c.SwaggerEndpoint("/Nevermind/swagger/v1/swagger.json", "Nevermind V1");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().DisableAntiforgery();

app.MapControllers();

app.Run();
