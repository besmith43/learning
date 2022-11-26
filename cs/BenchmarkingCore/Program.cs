using System;
using System.Collections.Generic;

namespace BenchmarkingCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new IsPrime();
            bool result;
            List<int> FinalList = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                result = test.TestForPrime(i);
                
                if (result)
                {
                    FinalList.Add(i);
                }
            }

            foreach (var num in FinalList)
            {
                Console.WriteLine(num);
            }
        }
    }
}
