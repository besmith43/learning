using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace async2
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWork t = new DoWork();
            t.startWork();
        }
    }

    class DoWork
    {
        public async void startWork()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(printWorkAsync(0));
            tasks.Add(printWorkAsync(1));

            await Task.WhenAll(tasks);
        }

        private async Task printWorkAsync(int num)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(num);
            }
        }
    }
}
