using Prise.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var pluginServiceCollection = new ServiceCollection()
	.AddPrise();

var pluginServiceProvider = pluginServiceCollection.BuildServiceProvider();

var pathToDist = Path.GetFullPath("../../../../Plugins/_dist", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

app.MapGet("/", () => {
	var pluginLoader = pluginServiceProvider.GetService<IPluginLoader>();

	var scanResult = pluginLoader.FindPlugin<IGetPlugin>(pathToDist).Result;

	if (scanResult != null)
	{
		var plugin = pluginLoader.LoadPlugin<IGetPlugin>(scanResult).Result;
		return plugin.Run();
	}

	return "no plugin found";
});

app.Run();
