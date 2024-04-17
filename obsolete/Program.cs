using System;

namespace obsolete
{
    class Program
    {
        // Mark Method1 obsolete without a message.
        [ObsoleteAttribute]
        public static string Method1()
        {
            return "You have called Method1.";
        }
 
        // Mark Method2 obsolete with a warning message.
        [ObsoleteAttribute("This method will soon be deprecated. Use MethodNew instead.")]
        public static string Method2()
        {
            return "You have called Method2.";
        }
 
        // Mark Method3 obsolete with an error message.
        [ObsoleteAttribute("This method has been deprecated. Use MethodNew instead.", true)]
        public static string Method3()
        {
            return "You have called Method3.";
        }
 
        public static string MethodNew()
        {
            return "You have called MethodNew.";
        }
 
        public static void Main()
        {
            Console.WriteLine(Method1());
            Console.WriteLine();
            Console.WriteLine(Method2());
            Console.WriteLine();
            Console.WriteLine(Method3());
        }
    }
}
