using System;

// see https://www.arclab.com/en/kb/csharp/global-variables-fields-functions-static-class.html
// for more info

namespace UsingGlobals
{
    static class Globals
    {
        public static int counter;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Globals.counter = 42;
            Console.WriteLine(Globals.counter);
        }
    }
}
