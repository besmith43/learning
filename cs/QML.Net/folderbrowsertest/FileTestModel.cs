using System;

namespace folderbrowsertest
{
    public class FileTestModel
    {
        private string filename = "";

        public void SetFile(string chosenFilename)
        {
            filename = chosenFilename;
            Console.WriteLine(filename);
        }

        public string GetFile()
        {
            return filename;
        }
    }
}