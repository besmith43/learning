using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                var server = new NamedPipeServerStream("TestService", PipeDirection.In);
                
                Console.WriteLine("Waiting for Connection");
                server.WaitForConnection();

                Console.WriteLine("Client Connected");
                try
                {
                    using (StreamReader sr = new StreamReader(server))
                    {
                        string temp;
                        while((temp = sr.ReadLine()) != null)
                        {
                            Console.WriteLine("Received from client: {0}", temp);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
        }
    }
}
