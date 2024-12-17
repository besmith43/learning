#!/usr/bin/env dotnet-script

#define DEBUG


#if DEBUG
	Console.WriteLine("Debug");
#else
	Console.WriteLine("Release");
#endif
