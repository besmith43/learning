using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What would you like to say to the Server?");
            string message = Console.ReadLine();

            NamedPipeClientStream client = new NamedPipeClientStream(".", "TestService", PipeDirection.Out, PipeOptions.Asynchronous);

            Console.WriteLine("Attempting to connect to the server");
            client.Connect();

            Console.WriteLine("Connected to Server");

            using (StreamWriter sw = new StreamWriter(client))
            {
                sw.AutoFlush = true;
                sw.WriteLine(message);
            }
        }
    }
}
