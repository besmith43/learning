using System;
using System.Runtime.InteropServices;

namespace cs_program
{
    class Program
    {
		[DllImport("exportgo.dll", EntryPoint="PrintBye")]
		static extern void PrintBye();

		[DllImport("exportgo.dll", EntryPoint="Sum")]
		static extern int Sum(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
			PrintBye();
			Console.WriteLine(Sum(32,22));
        }
    }
}
