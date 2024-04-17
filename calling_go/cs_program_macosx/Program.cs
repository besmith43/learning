using System;
using System.Runtime.InteropServices;

[DllImport("exportgo.dll", EntryPoint="PrintBye")]
static extern void PrintBye();

[DllImport("exportgo.dll", EntryPoint="Sum")]
static extern int Sum(int a, int b);

PrintBye();
Console.WriteLine(Sum(33,22));