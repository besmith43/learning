using System;
using System.Reflection;

namespace CakeProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var versionNumber = currentAssembly.GetName().Version;

            var fileVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version; //AssemblyFileVersionAttribute.Version;

            Console.WriteLine(versionNumber);
            Console.WriteLine(fileVersion);

            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"arg { i }: { args[i] }");
                }
            }
        }
    }
}
