// See https://aka.ms/new-console-template for more information
using static MonitoringLib.Recorder;
using static MonitoringLib.ThreadInfo;

Start();
OutputThreadInfo();
Console.WriteLine("Hello, World!");
Stop();
