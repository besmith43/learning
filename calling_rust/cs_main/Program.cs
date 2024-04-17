using System;
using System.Text;
using System.Runtime.InteropServices;

namespace cs_main
{
    class Program
    {
        //[DllImport("test.dll", CallingConvention = CallingConvention.Cdecl)]
		[DllImport("libtest.dylib", CallingConvention = CallingConvention.Cdecl)]
        static extern void lib_test();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from C#!");
            lib_test();
        }
    }
}
