using System;
using System.ServiceProcess;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What Command would you like to perform?");
            Console.WriteLine("1. Restart Computer");
            Console.WriteLine("2. Save Message to Log");
            Console.WriteLine("3. Update EagleNet Password");
            string choice = Console.ReadLine();

            if (choice == "1" | choice == "2" | choice == "3")
            {
                ServiceController myService = new ServiceController("ServiceConsoleTest");

                if (choice == "1")
                {
                    myService.ExecuteCommand((int)1);
                }
                else if (choice == "2")
                {
                    myService.ExecuteCommand((int)2);
                }
                else if (choice == "3")
                {
                    myService.ExecuteCommand((int)3);
                }
                else
                {
                    Console.WriteLine("You messed up big time");
                }
            }
            else
            {
                Console.WriteLine("You picked an invalid option. \nPlease try again.");
            }
        }
    }
}
