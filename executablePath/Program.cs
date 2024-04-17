using System;
using System.IO;
using System.Diagnostics;

namespace executablePath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Assembly Location: { Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) }");
            Console.WriteLine($"Environment Current Directory: { Environment.CurrentDirectory }");
            Console.WriteLine($"Assembly Codebase: { Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) }");
            //Console.WriteLine($"Application Executable Path: { Path.GetDirectoryName(Application.ExecutablePath) }");
            Console.WriteLine($"Command Line Path: { System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) }");
            Console.WriteLine($"System Current Directory: { System.IO.Directory.GetCurrentDirectory() }");
            Console.WriteLine($"Process Filename: { Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) }");
            Console.ReadLine();
        }
    }
}
