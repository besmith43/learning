#!/usr/bin/env dotnet-script


#r "nuget: SSH.NET, 2024.2.0"


using Renci.SshNet;


var host = "minim1";
// var host = "100.120.145.99"; // why doesn't the ip address work?
var user = "besmith";
var sshKey = "/Users/besmith/.ssh/id_rsa";
var port = 22;

var localDir = "./test";

using (var client = new ScpClient(host, port, user, new PrivateKeyFile(sshKey)))
{
    if (!Directory.Exists(localDir))
    {
        Directory.CreateDirectory(localDir);
    }
    else
    {
        ClearDir(localDir);
    }

    client.Connect();
    client.Download("Downloads/test", new DirectoryInfo(localDir));

    foreach (var dir in Directory.GetDirectories(localDir))
    {
        Console.WriteLine($"Test Directory: {dir}");
    }

    foreach (var file in Directory.GetFiles(localDir))
    {
        Console.WriteLine($"Test File: {file}");
    }

    ClearDir(localDir);
}

void ClearDir(string dir)
{
    Directory.Delete(dir, true);
}
