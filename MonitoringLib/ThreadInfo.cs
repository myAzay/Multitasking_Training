using static System.Console;

namespace MonitoringLib
{
    public static class ThreadInfo
    {
        public static void OutputThreadInfo()
        {
            Thread t = Thread.CurrentThread;
            WriteLine("Thread Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
                t.ManagedThreadId, t.Priority,
                t.IsBackground, t.Name ?? "null");
        }
    }
}
