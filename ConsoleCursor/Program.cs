using System;
using System.Threading;

namespace ConsoleCursor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("This");
			Thread.Sleep(2000);
			Console.Write("\rShould");
			Thread.Sleep(2000);
			Console.Write("\rWork    ");
			Thread.Sleep(2000);
			Console.Write("\rYAY!!!\n");
			Thread.Sleep(500);

			for (int i=0; i<24; i++)
			{
				Console.WriteLine("YAY!!!");
				Thread.Sleep(500);
			}
        }
    }
}
