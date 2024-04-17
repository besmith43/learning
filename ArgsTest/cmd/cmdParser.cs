using System;

namespace ArgsTest.cmd
{
    public class cmdParser
    {
        private string[] _args;
        private Options opts;

        public cmdParser(string[] Args)
        {
            _args = Args;
        }

        public Options Parse()
        {
            opts = new();

            if (_args != null)
            {
                for (int i = 0; i < _args.Length; i++)
                {
                    Console.WriteLine($"Argument { i }:  { _args[i] }");

                    if (_args[i] == "-h" || _args[i] == "--help")
                    {
                        opts.helpFlag = true;
                    }
                    else if (_args[i] == "-V" || _args[i] == "--version")
                    {
                        opts.versionFlag = true;
                    }
                    else if (_args[i] == "-v" || _args[i] == "--verbose")
                    {
                        opts.verbose = true;
                    }
                    else if (_args[i] == "-f" || _args[i] == "--file")
                    {
                        opts.file = _args[i+1];
                        i++;
                    }
                    else if (_args[i] == "-d" || _args[i] == "--directory")
                    {
                        opts.folder = _args[i+1];
                        i++;
                    }
                    else
                    {
                        // place any unnamed or positional value checks
                    }
                }
            }

            return opts;
        }
    }
}