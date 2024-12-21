using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http.Resilience;
using System.Net.Http;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddHttpClient("LocalPhpClient", client =>
{
    // Sets base URL for the weather API
    client.BaseAddress = new Uri("http://localhost:8443");
})
.AddResilienceHandler("MyResilienceStrategy", resilienceBuilder => // Adds resilience policy named "MyResilienceStrategy"
{
    // Retry Strategy configuration
    resilienceBuilder.AddRetry(new HttpRetryStrategyOptions // Configures retry behavior
    {
        MaxRetryAttempts = 4, // Maximum retries before throwing an exception (default: 3)

        Delay = TimeSpan.FromSeconds(2), // Delay between retries (default: varies by strategy)

        BackoffType = DelayBackoffType.Exponential, // Exponential backoff for increasing delays (default)

        UseJitter = true, // Adds random jitter to delay for better distribution (default: false)

        ShouldHandle = new PredicateBuilder<HttpResponseMessage>() // Defines exceptions to trigger retries
        .Handle<HttpRequestException>() // Includes any HttpRequestException
        .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
    });
});

var app = builder.Build();

app.AddCommand(
(
	IHttpClientFactory httpClientFactory
)
=>
{
	var httpClient = httpClientFactory.CreateClient("LocalPhpClient");
	var response = await httpClient.GetAsync("index.php").Result;

	Console.WriteLine("Http Response: " + response.ToString());
});

app.Run();


