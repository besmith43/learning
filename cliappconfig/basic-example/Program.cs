using System;
using Xdg.Directories;

// Prints /home/$USER/.local/share
Console.WriteLine(BaseDirectory.DataHome);

// Prints /home/$USER/.cache
Console.WriteLine(BaseDirectory.CacheHome);

// Prints /home/$USER
Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

// Prints AppName
Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName}");


Console.WriteLine($"Usage: {System.Diagnostics.Process.GetCurrentProcess().ProcessName}");


Console.WriteLine($"Usage: {Path.GetFileName(Environment.GetCommandLineArgs()[0])}");



