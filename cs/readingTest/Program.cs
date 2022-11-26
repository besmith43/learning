using System;
using System.Threading;

namespace readingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s = Console.In.ReadToEnd();
            string s = Console.ReadLine();
            Console.WriteLine(s);

            while(true)
            {
                int i = Console.Read();
                Console.WriteLine(i);
                Thread.Sleep(250);
            }
        }
    }
}
