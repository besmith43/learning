using System;
using System.Drawing;
using Console = Colorful.Console;

//see http://colorfulconsole.com/ for more info

namespace ColorfulConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteAscii("HASSELHOFF", Color.FromArgb(DA, V, ID));

                DA -= 18;
                V -= 36;
            }
        }
    }
}
