using System;
using System.IO;

namespace bundleExe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Base Directory Value: { System.AppContext.BaseDirectory }");
            Console.WriteLine($"Path of Base Directory: { Path.GetDirectoryName(System.AppContext.BaseDirectory) }");

            if (File.Exists($"{Path.GetDirectoryName(System.AppContext.BaseDirectory)}/resources/test.txt"))
            {
                Console.WriteLine("Test File is there");
            }
            else
            {
                Console.WriteLine("Test File is not there");
            }
            Console.ReadLine();
        }
    }
}
