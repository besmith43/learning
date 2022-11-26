using System;
using System.Management.Automation;

namespace Calling_PS
{
    class Program
    {
        static void Main(string[] args)
        {
            // create empty pipeline
            var results = PowerShell.Create().AddCommand("Get-Process").AddParameter("Name", "PowerShell").Invoke();

            foreach( PSObject result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Hit any key to quit");
            Console.ReadKey();
        }
    }
}
