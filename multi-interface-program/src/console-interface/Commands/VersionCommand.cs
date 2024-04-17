using System;
using System.Reflection;
using System.Threading.Tasks;

namespace console_interface.Commands
{
    public class VersionCommand : ICommand
    {
		public const string COMMAND_NAME = "version";
		public string versionText = $"Console Interface { Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version }";

        public Task<bool> ExecuteAsync()
		{
			return Task.Run(() => {
				Console.WriteLine(versionText);
				return true;
			});
		}
    }
}
