// See https://aka.ms/new-console-template for more information
using App.Helpers;
using static MonitoringLib.ResourceMonitoring.Recorder;
using static MonitoringLib.ThreadMonitoring.ThreadInfo;

Test2();

void Test1()
{
    Start();

    Thread currentThread = Thread.CurrentThread;
    currentThread.Name = "MainThread";

    Thread thread = new Thread(TestSimpleLoop);
    thread.Priority = ThreadPriority.Highest;
    thread.IsBackground = true;
    thread.Name = "MySecondThread";

    Thread parameterizedThread = new Thread(TestParameterizedThread);
    parameterizedThread.Name = "ParameterizedThread";

    thread.Start();
    parameterizedThread.Start(100);
    TestSimpleLoop();
    
    parameterizedThread.Join();
    thread.Join();

    Console.WriteLine("End");
    Stop();
}

void Test2()
{
    Start();

    var sharedObject = new SharedObject();

    Task firstTask = Task.Run(sharedObject.WriteA);
    Task secondTask = Task.Run(sharedObject.WriteB);

    Console.WriteLine("FirstTest");
    Task.WaitAll(new Task[] { firstTask, secondTask });
    Console.WriteLine(sharedObject.Message);
    sharedObject.Message = "";
    Console.WriteLine();

    Task firstTaskWithObjectLock = Task.Run(sharedObject.WriteAWithLock);
    Task secondTaskWithObjectLock = Task.Run(sharedObject.WriteBWithLock);

    Console.WriteLine("SecondTest");
    Task.WaitAll(new Task[] { firstTaskWithObjectLock, secondTaskWithObjectLock });
    Console.WriteLine(sharedObject.Message);

    Console.WriteLine();
    Stop();
}

void TestSimpleLoop()
{
    for (int i = 0; i < 100; i++)
    {
        OutputThreadInfo();
    }
}

void TestParameterizedThread(object? obj)
{
    
    try
    {
        if (obj is null)
            return;

        int iterator = (int)obj;
        for (int i = 0; i < iterator; i++)
        {
            OutputThreadInfo();
        }
    }
    catch (InvalidCastException e)
    {
        throw new InvalidCastException("Error: Incorrect unboxing.", e);
    }
}
