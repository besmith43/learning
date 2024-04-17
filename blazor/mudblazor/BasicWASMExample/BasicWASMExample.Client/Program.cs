using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BasicWASMExample.Client.APIReceivers;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => 
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

// builder.Services.AddHttpClient();

// this is registered, but now it's saying that there is no registered service  of type QuickLinksService
// builder.Services.AddSingleton<IAPIService, QuickLinksService>();

await builder.Build().RunAsync();
