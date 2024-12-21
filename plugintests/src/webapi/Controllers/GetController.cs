using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Prise;
using plugin.contract;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class GetController : ControllerBase
{
    private readonly ILogger<GetController> _logger;
	private readonly IPluginLoader pluginLoader;

    public GetController(ILogger<GetController> logger, IPluginLoader pluginLoader)
    {
        _logger = logger;
		this.pluginLoader = pluginLoader;
    }

    [HttpGet(Name = "GetStatement")]
    public async Task<IEnumerable<GetPlugin>> Get()
    {
		var pathToBinDebug = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		var pathtoDist = Path.GetFullPath("../../../../Plugins/_dist", pathToBinDebug);
		_logger.LogDebug("Looking for plugins in {0}", pathtoDist);
		var scanResult = await this.pluginLoader.FindPlugin<IGetPlugin>(pathtoDist);

		if (scanResult == null)
		{
			_logger.LogError("No plugin found");
			return null;
		}

		var plugin = await this.pluginLoader.LoadPlugin<IGetPlugin>(scanResult);

		return await plugin.Run();
    }
}