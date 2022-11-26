using System;
using System.Management;

namespace checkManufacturer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CheckManufacturer())
            {
                Console.WriteLine("Made by Lenovo");
            }
            else
            {
                Console.WriteLine("Not Made by Lenovo");
            }
        }

        public static bool CheckManufacturer()
		{
            System.Diagnostics.ProcessStartInfo usbDevicesInfo = new System.Diagnostics.ProcessStartInfo("wmic", "computersystem get manufacturer");
            usbDevicesInfo.RedirectStandardOutput = true;
            usbDevicesInfo.UseShellExecute = false;
            usbDevicesInfo.CreateNoWindow = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = usbDevicesInfo;
            process.Start();
            process.WaitForExit();
            Console.WriteLine("ExitCode: " + process.ExitCode.ToString() + "\n");
            var result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(result);

            if (result.Contains("LENOVO"))
			{
                return true;
			}
			else
			{
                return false;
			}
		}
    }
}
