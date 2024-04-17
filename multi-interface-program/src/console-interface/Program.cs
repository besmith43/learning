using System;
using System.Linq;
using console_interface.Commands;

namespace console_interface
{
    class Program
    {
        static void Main(string[] args)
        {
			if (args.Length == 0)
            {
                args = new string[] { "help" };
            }

            ICommand command = null;

            switch (args[0].ToLower())
            {
				case AddCommand.COMMAND_NAME:
					command = new AddCommand(args.Skip(1).ToArray());
                    break;
				case SubCommand.COMMAND_NAME:
					command = new SubCommand(args.Skip(1).ToArray());
                    break;
				case MulCommand.COMMAND_NAME:
					command = new MulCommand(args.Skip(1).ToArray());
                    break;
				case DivCommand.COMMAND_NAME:
					command = new DivCommand(args.Skip(1).ToArray());
                    break;
                case VersionCommand.COMMAND_NAME:
                    command = new VersionCommand();
                    break;
                case "--help":
                case "-h":
                case HelpCommand.COMMAND_NAME:
                    command = new HelpCommand(args.Skip(1).ToArray());
                    break;
                default:
                    Console.Error.WriteLine($"Unknown command {args[0]}");
                    Environment.Exit(-1);
                    break;
            }

            Run(command);
        }

        public static void Run(ICommand cmd)
        {
            if (cmd != null)
            {
                var success = cmd.ExecuteAsync().Result;

                if (!success)
                {
                    Environment.Exit(-1);
                }
			}
        }
    }
}
