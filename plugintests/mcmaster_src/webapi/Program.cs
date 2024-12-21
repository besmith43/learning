using McMaster.NETCore.Plugins;
using contract;
using System.Reflection;
using System.Linq;
using Serilog;

Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

var LoggerConfig = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(LoggerConfig)
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var loaders = new List<PluginLoader>();

var pluginsDir = Path.GetFullPath("../dist", Directory.GetCurrentDirectory());

Log.Debug($"pluginsDir: {pluginsDir}");

foreach (var dir in Directory.GetDirectories(pluginsDir))
{
	Log.Debug(dir);
	var dirName = Path.GetFileName(dir);
	Log.Debug(dirName);
	var pluginDll = Path.Combine(dir, dirName + ".dll");
	Log.Debug(pluginDll);

	if (File.Exists(pluginDll))
	{
		var loader = PluginLoader.CreateFromAssemblyFile(
			pluginDll,
			sharedTypes: new[] {
				typeof(IPlugin),
				typeof(IServiceCollection)
			},
			config => config.EnableHotReload = true
		);
		loaders.Add(loader);
	}
}

app.MapGet("/", () => "Hello World!");


app.MapGet("/1", () =>
{
	foreach (var loader in loaders)
	{
		foreach (var pluginType in loader
			.LoadDefaultAssembly()
			.GetTypes()
			.Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract))
		{
			var plugin = Activator.CreateInstance(pluginType) as IPlugin;

			Log.Debug($"created plugin instance '{plugin?.GetName()}'.");
			// return $"created plugin instance '{ plugin?.GetName() }'.";
		}
	}

	return "no plugin found";
});


app.MapGet("/2", () =>
{
	try
	{
		var plugin = getPlugin("Plugin2");

		if (plugin == null)
		{
			Log.Debug($"{System.DateTime.Now} - plugin not found.");
			return "plugin not found";
		}

		var result = plugin.GetName();
		Log.Debug($"{System.DateTime.Now} - created plugin instance '{ result }'.");
		return $"created plugin instance '{ result }'.";
	}
	catch (Exception ex)
	{
		Log.Debug($"{System.DateTime.Now} - exception: {ex.Message}");
		return $"exception: {ex.Message}";
	}

});

app.Run();



IPlugin? getPlugin(string pluginName)
{
	var pathToPlugin = Path.GetFullPath($"../../../../dist/{ pluginName }/{ pluginName }.dll", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

	var loader = PluginLoader.CreateFromAssemblyFile(
		pathToPlugin,
		sharedTypes: new[] { typeof(IPlugin) },
		config => config.EnableHotReload = true
	);

	var assembly = loader.LoadDefaultAssembly()
		.GetTypes()
		.Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);

	Log.Debug($"assembly count: {assembly.Count()}");

	var plugin = Activator.CreateInstance(assembly.SingleOrDefault()) as IPlugin;
	
	return plugin;
}