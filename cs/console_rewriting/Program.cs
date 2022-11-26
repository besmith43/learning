using System;
using System.Threading;

namespace console_rewriting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("\n\n");

            (int left, int top) cursorPosition = Console.GetCursorPosition();

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, cursorPosition.top);
                Console.Write($"Testing Loop { i+1 }");
                Thread.Sleep(1000);
            }
        }
    }
}
