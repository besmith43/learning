using System;

namespace BitShift
{
    class Program
    {
        static void Main(string[] args)
        {
            // taken from https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators

            // bitwise complement
            uint a = 0b_0000_1111_0000_1111_0000_1111_0000_1100;
            uint b = ~a;
            Console.WriteLine(Convert.ToString(b, toBase: 2));

            // left shift
            uint x = 0b_1100_1001_0000_0000_0000_0000_0001_0001;
            Console.WriteLine($"Before: {Convert.ToString(x, toBase: 2)}");

            uint y = x << 4;
            Console.WriteLine($"After:  {Convert.ToString(y, toBase: 2)}");

            // right shift
            x = 0b_1001;
            Console.WriteLine($"Before: {Convert.ToString(x, toBase: 2), 4}");

            y = x >> 2;
            Console.WriteLine($"After:  {Convert.ToString(y, toBase: 2), 4}");

            // logical AND
            a = 0b_1111_1000;
            b = 0b_1001_1101;
            uint c = a & b;
            Console.WriteLine(Convert.ToString(c, toBase: 2));

            // logical exclusive OR
            a = 0b_1111_1000;
            b = 0b_0001_1100;
            c = a ^ b;
            Console.WriteLine(Convert.ToString(c, toBase: 2));

            // logical OR
            a = 0b_1010_0000;
            b = 0b_1001_0001;
            c = a | b;
            Console.WriteLine(Convert.ToString(c, toBase: 2));
        }
    }
}
