using System;
using System.Runtime.InteropServices;

namespace cs_program_linux
{
    class Program
    {
        [DllImport("exportgo.so", EntryPoint="PrintBye")]
        static extern void PrintBye();

        [DllImport("exportgo.so", EntryPoint="Sum")]
        static extern int Sum(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PrintBye();
            Console.WriteLine(Sum(32,22));
        }
    }
}
