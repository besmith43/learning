using System;
using System.Text;
using System.Runtime.InteropServices;

namespace UseCppDllFromCSharp
{
    class Program
    {
        [DllImport("shared_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void pipecommand(string str);
        [DllImport("shared_lib.dll", CharSet = CharSet.Ansi)]
        static extern void StrCat(string str1, string str2, StringBuilder newstr);
        [DllImport("shared_lib.dll", CharSet = CharSet.Ansi)]
        static extern void StrCpy(StringBuilder str1, string str2);
        [DllImport("shared_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void SaySomething(string str);
        [DllImport("shared_lib.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int add(int a, int b);


        static void Main(string[] args)
        {
            int c = add(25,25);
            Console.WriteLine("c=" + c);

            StringBuilder sb = new StringBuilder();
            StrCat("hello", "world", sb);
            Console.WriteLine("str is " + sb.ToString());

            StrCpy(sb, "the dog");
            Console.WriteLine("str is " + sb.ToString());

            pipecommand(@"dir c:\");

            SaySomething("how are you?");

            Console.ReadLine();
        }
    }
}
