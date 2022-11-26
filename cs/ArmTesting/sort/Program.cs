using System;
using System.Diagnostics;
using CommandLine;

namespace sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        private static void Run(Options opts)
        {
            if (opts.max == 0)
            {
                opts.max = opts.size;
            }

            Random randomGen = new Random();
            int[] randArr = new int[opts.size];

            for(int i = 0; i < opts.size; i++)
            {
                randArr[i] = randomGen.Next(0, opts.max);
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Array.Sort(randArr, 0, opts.size);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            if (opts.verbose)
            {
                for (int i = 0; i < opts.size; i++)
                {
                    Console.WriteLine($"Index{i}: {randArr[i]}");
                }
            }

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("RunTime " + elapsedTime);
        }
    }

    public class Options
    {
        [Value(0, MetaName = "Size", Required = true, HelpText = "Size of the Array")]
        public int size { get; set; }

        [Value(1, MetaName = "Largest Number", Required = false, HelpText = "The largest number possible")]
        public int max { get; set; }

        [Option('v',"verbose", Required = false, HelpText = "Outputs the contents of the Array after sorting")]
        public bool verbose { get; set; }
    }
}
