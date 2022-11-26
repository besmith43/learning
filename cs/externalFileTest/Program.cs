using System;
using System.IO;

namespace externalFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("\n\n");

			string pwd = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string scriptDirectory = pwd + "\\scripts";

			Console.WriteLine(scriptDirectory);

			if (Directory.Exists(scriptDirectory))
			{
				Console.WriteLine("Script Folder Exists");
			}
			else
			{
				Console.WriteLine("Script Folder Doesn't Exist");
			}

			string singleScript = scriptDirectory + "\\new-page.ps1";

			Console.WriteLine(singleScript);

			if (File.Exists(singleScript))
			{
				Console.WriteLine("New-Page Script Exists");
			}
			else
			{
				Console.WriteLine("New-Page Script Doesn't Exist");
			}

			string scriptDirectory2 = ".\\scripts";

			Console.WriteLine(scriptDirectory2);

			if (Directory.Exists(scriptDirectory2))
			{
				Console.WriteLine("Script Folder Exists");
			}
			else
			{
				Console.WriteLine("Script Folder Doesn't Exist");
			}

			string singleScript2 = ".\\scripts\\new-page.ps1";

			Console.WriteLine(singleScript2);

			if (File.Exists(singleScript2))
			{
				Console.WriteLine("New-Page Script Exists");
			}
			else
			{
				Console.WriteLine("New-Page Script Doesn't Exist");
			}

			Console.ReadLine();
        }
    }
}
