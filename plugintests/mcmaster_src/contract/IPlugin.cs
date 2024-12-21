namespace contract;
public interface IPlugin
{
	string GetName();
	void ConfigureServices(IServiceCollection services);
}
