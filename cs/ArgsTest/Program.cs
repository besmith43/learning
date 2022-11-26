using System;
using ArgsTest.cmd;

namespace ArgsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            cmdParser cmdP = new(args);

            Options cmdFlags = cmdP.Parse(); 
        }
    }
}
