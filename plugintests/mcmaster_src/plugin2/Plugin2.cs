using contract;

namespace plugin2;

public class Plugin2 : contract.IPlugin
{
	public string GetName()
	{
		log.Debug("Plugin2.GetName() called.");
		return "Plugin2";
		// return "Plugin2 now with more cowbell!";
	}
}
