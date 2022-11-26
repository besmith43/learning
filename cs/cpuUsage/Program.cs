using System;
using System.Diagnostics;

// see https://stackoverflow.com/questions/278071/how-to-get-the-cpu-usage-in-c
// and https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecounter?redirectedfrom=MSDN&view=netcore-3.0
// for more info

namespace cpuUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((int)getCPUCounter());
        }

        public object getCPUCounter()
        {

            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            // will always start at 0
            dynamic firstValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            // now matches task manager reading
            dynamic secondValue = cpuCounter.NextValue();

            return secondValue;

        }
    }
}
