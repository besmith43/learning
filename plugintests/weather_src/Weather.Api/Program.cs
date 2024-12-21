using System.Reflection;
using Prise.DependencyInjection;
using Prise;
using Weather.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPrise();

var app = builder.Build();

app.MapGet("/", async (HttpContext context, IConfiguration config) => {
	var weatherPluginLoader = context.RequestServices.GetService<IPluginLoader>();
	var pathToBinDebug = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
	var pathToDist = Path.GetFullPath("../../../../dist", pathToBinDebug);
	var scanResult = await weatherPluginLoader.FindPlugin<IWeatherPlugin>(pathToDist);

	if (scanResult == null)
		return "No plugin found";

	// var plugin = await weatherPluginLoader.LoadPlugin<IWeatherPlugin>(scanResult);

	// return await plugin.GetWeatherForecast();
	return "Plugin was found";
});

app.Run();
