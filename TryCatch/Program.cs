using System;

// see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch
// for more info

namespace TryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            object o2 = null;
            try
            {
                int i2 = (int)o2;   // Error
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("invalid cast exception ", e);
            }
            catch (ArgumentException e) when (e.ParamName == "…")
            {
                Console.WriteLine("argument exception");
            }
        }
    }
}
