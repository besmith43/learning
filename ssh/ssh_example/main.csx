#!/usr/bin/env dotnet-script


#r "nuget: SSH.NET, 2024.2.0"


using Renci.SshNet;


var host = "minim1";
// var host = "100.120.145.99"; // why doesn't the ip address work?
var user = "besmith";
var sshKey = "/Users/besmith/.ssh/id_rsa";
var port = 22;

using (var client = new SshClient(host, port, user, new PrivateKeyFile(sshKey)))
{
    client.Connect();
    using SshCommand cmd = client.RunCommand("ls ~/Downloads");
    Console.WriteLine(cmd.Result); // "Hello World!\n"
}
