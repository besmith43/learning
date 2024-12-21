using Prise.Plugin;
using plugin.contract;

namespace prise_plugin2;

[Plugin(PluginType = typeof(IGetPlugin))]
public class Plugin2 : IGetPlugin
{
	public async Task<IEnumerable<GetPlugin>> Run()
	{
		return await Task.FromResult(new[] { new GetPlugin { Statement = "Hello from Plugin 2" } });
	}
}

