using System;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using doWorkLib;

namespace console_interface.Commands
{
    public class DivCommand : ICommand
    {
		public const string COMMAND_NAME = "div";
		public bool helpFlag = false;
		public ICommand VersionCommand = new VersionCommand();
		public int? a = null;
		public int? b = null;

		public DivCommand(string[] _args)
		{
			ParseArgs(_args);
		}

        public Task<bool> ExecuteAsync()
		{
			return Task.Run(() => {
				
				if (helpFlag)
				{
					printHelp();
				}
				
				if (a != null && b != null)
				{
					int c = SharedClass.div(a.Value, b.Value);
					Console.WriteLine(c);
				}
				else
				{
					Console.WriteLine("One of the parameters wasn't given");
					return false;
				}

				return true;
			});
		}

		private void ParseArgs(string[] args)
		{
			for(int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h" || args[i] == "--help")
                {
					helpFlag = true;
				}
				else
				{
					if (a == null)
					{
						try
						{
							a = Int32.Parse(args[i]);
						}
						catch (FormatException e)
						{
							Console.WriteLine("Input was not a number");
							Console.WriteLine(e);
						}
					}
					else if (b == null)
					{
						try
						{
							b = Int32.Parse(args[i]);
						}
						catch (FormatException e)
						{
							Console.WriteLine("Input was not a number");
							Console.WriteLine(e);
						}
					}
				}
			}
		}

		public void printHelp()
		{
			StringBuilder helpTextBuilder = new StringBuilder();

           	helpTextBuilder.AppendLine("Usage:  Console-Interface div [OPTION] <int> <int> ");
			helpTextBuilder.AppendLine("");
            helpTextBuilder.AppendLine("    -V | --verbose          Set verbose mode");
           	helpTextBuilder.AppendLine("    -v | --version          Display version message");
           	helpTextBuilder.AppendLine("    -h | --help             Display this help message");
           	helpTextBuilder.AppendLine("");

			VersionCommand.ExecuteAsync();
           	Console.WriteLine(helpTextBuilder.ToString());
		}
    }
}
