// See https://aka.ms/new-console-template for more information
using static MonitoringLib.ResourceMonitoring.Recorder;
using static MonitoringLib.ThreadMonitoring.ThreadInfo;

Start();
Thread thread = new Thread(TestSimpleLoop);
thread.Priority = ThreadPriority.Highest;
thread.IsBackground = true;
thread.Name = "MySecondThread";
thread.Start();
TestSimpleLoop();
Console.WriteLine("End");
Stop();

void TestSimpleLoop()
{
    for (int i = 0; i < 100; i++)
    {
        OutputThreadInfo();
    }
}
