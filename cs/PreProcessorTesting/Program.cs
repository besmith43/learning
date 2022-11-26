#define VariableName
#undef ERROR

// these must be at the top of the file

using System;

// see https://www.programiz.com/csharp-programming/preprocessor-directives for more info
// see also https://khalidabuhakmeh.com/csharp-preprocessor-directives for more info

namespace PreProcessorTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #if VariableName
                Console.WriteLine("VariableName is a preprocessor defined variable flag");
            #elif ERROR
                #error This will throw a compiler error message
            #else
                Console.WriteLine("It's not defined");
                #warning Throwing a compiler warning error
            #endif

            #if NET5_0
                Console.WriteLine("This is running in .Net 5.0");
            #elif NETCOREAPP
                Console.WriteLine("This is running in .Net Core");
            #elif NETFRAMEWORK
                Console.WriteLine("This is running in .Net Framework");
            #else
                Console.WriteLine("No idea if this will ever run");
            #endif
        }
    }
}
