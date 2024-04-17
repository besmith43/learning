using System;
using System.Threading.Tasks;
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

            if (opts.threads == 0)
            {
                opts.threads = 1;
            }

            Random randomGen = new Random();
            int[][] randArr = new int[opts.threads][];

            for (int i = 0; i < opts.threads; i++)
            {
                randArr[i] = new int[opts.size];
            }

            for(int i = 0; i < opts.threads; i++)
            {
                for(int j = 0; j < opts.size; i++)
                {
                    randArr[i][j] = randomGen.Next(0, opts.max);
                }
            }

            Parallel.ForEach(randArr, (Arr) => {
                Array.Sort(Arr, 0, opts.size);
            });

            for (int i = 0; i < opts.threads; i++)
            {
                for (int j = 0; j < opts.size; j++)
                {
                    Console.WriteLine($"Thread{i}, Index{j} : {randArr[i][j]}");
                }
            }
        }
    }

    public class Options
    {
        [Value(0, MetaName = "Size", Required = true, HelpText = "Size of the Array")]
        public int size { get; set; }

        [Value(1, MetaName = "Largest Number", Required = false, HelpText = "The largest number possible")]
        public int max { get; set; }

        [Value(2, MetaName = "Threads", Required = false, HelpText = "Number of Threads to run consecutively")]
        public int threads { get; set; }
    }
}
