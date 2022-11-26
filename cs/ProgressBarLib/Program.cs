using System;
using ShellProgressBar;
using System.Threading;
using System.Threading.Tasks;

// see https://github.com/Mpdreamz/shellprogressbar for more info

namespace Progress.Bar
{
    class Program
    {
        static void Main(string[] args)
        {
            const int totalTicks = 10;
            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                ForegroundColorDone = ConsoleColor.DarkGreen,
                BackgroundColor = ConsoleColor.DarkGray,
                BackgroundCharacter = '\u2593'
            };
            using (var pbar = new ProgressBar(totalTicks, "showing off styling", options))
            {
                TickToCompletion(pbar, totalTicks, sleep: 500);
            }
        }


        static void TickToCompletion(IProgressBar pbar, int ticks, int sleep = 1750, Action childAction = null)
        {
            var initialMessage = pbar.Message;
            for (var i = 0; i < ticks; i++)
            {
                pbar.Message = $"Start {i + 1} of {ticks}: {initialMessage}";
                childAction?.Invoke();
                Thread.Sleep(sleep);
                pbar.Tick($"End {i + 1} of {ticks}: {initialMessage}");
            }
        }
    }
}
