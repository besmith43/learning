using System;
using System.IO;

//see https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netcore-3.0
//and https://docs.microsoft.com/en-us/dotnet/api/system.io.path?view=netcore-3.0
// for more info

namespace logging
{
    class Program
    {
        static void Main(string[] args)
        {
            var logPath = System.IO.Path.GetTempFileName();
            using (var writer = File.CreateText(logPath))
            {
                writer.WriteLine("log message");
            }

            Console.WriteLine(logPath);
        }
    }
}
