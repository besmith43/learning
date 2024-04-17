using System;
using System.Text;
using System.Reflection;

#nullable enable

namespace ArgsTest.cmd
{
    public class Options
    {
        public bool helpFlag { get; set; }
        public string helpText { get; set; }
        public bool versionFlag { get; set; }
        public string versionText { get; set; }
        public bool verbose { get; set; }
        public string file { get; set; }
        public string folder { get; set; }

        public Options()
        {
            helpFlag = false;
            versionFlag = false;
            versionText = $"FilmEditor { Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version }";
            verbose = false;
            StringBuilder helpTextBuilder = new StringBuilder();
            helpTextBuilder.AppendLine($"ArgsTest { versionText }");
            helpTextBuilder.AppendLine("Usage:  ArgsTest <Command> [OPTION]  ");
            helpTextBuilder.AppendLine("");
            helpTextBuilder.AppendLine("    -f | --file             Test File");
            helpTextBuilder.AppendLine("    -d | --directory        Test Directory");
            helpTextBuilder.AppendLine("    -V | --verbose          Set verbose mode");
            helpTextBuilder.AppendLine("    -v | --version          Display version message");
            helpTextBuilder.AppendLine("    -h | --help             Display this help message");
            helpTextBuilder.AppendLine("");
            helpText = helpTextBuilder.ToString();
        }
    }
}