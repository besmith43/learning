using System;
using System.Runtime.InteropServices;

namespace OSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (OperatingSystem.IsMacOS())
            {
                Console.WriteLine("We're on macOS!");
            }

            if (OperatingSystem.IsWindows())
            {
                Console.WriteLine("We're on Windows!");
            }

            if (OperatingSystem.IsLinux())
            {
                Console.WriteLine("We're on Linux!");
            }
        }
    }

    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
