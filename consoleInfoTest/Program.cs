using System;
using System.IO;

namespace consoleInfoTest
{
    class Program
    {
        public static string fileName = @".\test.txt";
        public static string[] fileContents;
        public static int topPosition = 0;
        public static bool quit = false;
        static void Main(string[] args)
        {
            fileContents = File.ReadAllLines(fileName);

            Init();

            do
            {
                getUserInput();
            } while (!quit);

            Console.Clear();
            Console.CursorVisible = true;
        }

        public static void Init()
        {
            Console.CursorVisible = false;

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.WriteLine(fileContents[i]);
            }
        }

        public static void getUserInput()
        {
            var keyInput = Console.ReadKey(true);

            if (Console.KeyAvailable == false)
            {
                switch (keyInput.Key)
                {
                    case ConsoleKey.J:
                    case ConsoleKey.DownArrow:
                        topPosition++;
                        Refresh();
                        break;
                    case ConsoleKey.K:
                    case ConsoleKey.UpArrow:
                        if (topPosition > 0)
                        {
                            topPosition--;
                            Refresh();
                        }
                        break;
                    case ConsoleKey.Q:
                        quit = true;
                        break;
                }
            }
        }

        public static void Refresh()
        {
            for (int i = topPosition; i < topPosition + Console.WindowHeight; i++)
            {
                Console.WriteLine(fileContents[i]);
            }
        }
    }
}
