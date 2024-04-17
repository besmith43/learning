using System;

namespace DoWhileLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quitFlag = false;
            string answer;

            do
            {
                Console.WriteLine("Do you want to quit?");
                answer = Console.ReadLine();
                answer = answer.Replace(Environment.NewLine, "");

                if (answer == "q")
                {
                    quitFlag = true;
                    Console.WriteLine("Change Quit Flag to True");
                }
            } while (!quitFlag);
        }
    }
}
