using System;
using System.Threading;

// see  https://www.youtube.com/watch?v=hOVSKuFTUiI for more info

namespace ThreadingEx
{
    // Simple threading scenario:  Start a static method running
    // on a second thread.
    public class ThreadExample
    {
        public static void Main()
        {
            Thread t = new Thread(Print1);

            t.Start();

            for (int i = 0; i < 1000; i++)
            {
                Console.Write(0);
            }

            Console.ReadLine();
        }

        public static void Print1()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(1);
            }
        }
    }
}
