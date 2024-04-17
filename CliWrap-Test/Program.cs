using System;
using CliWrap;

namespace CliWrap_Test
{
    class Program
    {
        static void Main(string[] args)
        {
			var command = Cli.Wrap("git")
				.WithArguments(a => a
						.Add("stats"))
				.WithEnvironmentVariables(e => e
						.Set("GIT_AUTHOR_NAME", "Blake Smith")
						.Set("GIT_AUTHOR_EMAIL", "besmith43@gmail.com"))
				.WithValidation(CommandResultValidation.None);

			var result = command.ExecuteAsync().Select(r => r.ExitCode);

			Console.WriteLine(result.Task.Result);
        }
    }
}
