using System;
using System.IO;

namespace PipeliningTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string awkExe = Environment.CurrentDirectory + "\\awk.exe";

            Console.WriteLine("cmd.exe /C arp -a 149.149.140.5 | findstr \"\\<149.149.140.5\\>\" | \"" + awkExe + "\" \"{print $2}\" ");

            var awkProcess = System.Diagnostics.Process.Start("cmd.exe","/C arp -a 149.149.140.5 | findstr \"\\<149.149.140.5\\>\" | \"" + awkExe + "\" \"{print $2}\" ");
            awkProcess.StartInfo.RedirectStandardOutput = true;
            awkProcess.Start();
            
            StreamReader reader = awkProcess.StandardOutput;
            string awkOutput = reader.ReadToEnd();
            awkProcess.WaitForExit();

            Console.WriteLine($"Awk Process output: { awkOutput }");
        }
    }
}
