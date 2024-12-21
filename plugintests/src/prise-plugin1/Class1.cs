using Prise.Plugin;
using plugin.contract;

namespace prise_plugin1;

[Plugin(PluginType = typeof(IGetPlugin))]
public class Plugin1 : IGetPlugin
{
	public async Task<IEnumerable<GetPlugin>> Run()
	{
		return await Task.FromResult(new[] { new GetPlugin { Statement = "Hello from Plugin 1" } });
	}
}
