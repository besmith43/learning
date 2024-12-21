using contract;

namespace plugin1;

public class Plugin1 : contract.IPlugin
{
	private readonly ILogger log;
	public string GetName()
	{
		log.Debug("Plugin1.GetName() called.");
		return "Plugin1";
	}
}
