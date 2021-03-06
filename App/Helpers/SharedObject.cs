using MonitoringLib.ThreadMonitoring;
using static System.Console;

namespace App.Helpers
{
    internal class SharedObject
    {
        public string Message {
            get => _message;
            private set => _message = value;
        }
        public int Counter
        {
            get => _counter;
            private set => _counter = value;
        }

        private readonly int _iterator;
        private string _message;
        private int _counter;

        public SharedObject(string message)
        {
            _message = message;
            _iterator = 5;
            _counter = 0;
        }

        public SharedObject() : this("") { }

        public void SetPropertiesToDefaultValue()
        {
            _message = "";
            _counter = 0;
        }
        public void WriteA()
        {
            for (int i = 0; i < _iterator; i++)
            {
                _message += "A";
                Interlocked.Increment(ref _counter);
                ThreadInfo.OutputThreadInfo();
            }
            WriteLine();
        }
        
        public void WriteB()
        {
            for (int i = 0; i < _iterator; i++)
            {
                _message += "B";
                Interlocked.Increment(ref _counter);
                ThreadInfo.OutputThreadInfo();
            }
            WriteLine();
        }

        public void WriteAWithLock()
        {
            lock (this)
            {
                for (int i = 0; i < _iterator; i++)
                {
                    _message += "A";
                    _counter++;
                    ThreadInfo.OutputThreadInfo();
                }
                WriteLine();
            }
        }

        public void WriteBWithLock()
        {
            lock (this)
            {
                for (int i = 0; i < _iterator; i++)
                {
                    _message += "B";
                    _counter++;
                    ThreadInfo.OutputThreadInfo();
                }
                WriteLine();
            }
        }

        /// <summary>
        /// Act the same way as WriteAWithLock function.
        /// <br/>
        /// Just to remember
        /// </summary>
        public void TestA()
        {
            /*
             * When a thread calls Monitor.Enter on any object, aka reference type, it checks to see if some 
             * other thread has already taken the conch. If it has, the thread waits. If it has not, the thread 
             * takes the object and gets on with its work on the shared resource. Once the thread has finished 
             * its work, it calls Monitor.Exit, releasing the object. If another thread was waiting, it can 
             * now take the object and do its work
             */
            try
            {
                //To avoid deadlock need use Monitor.TryEnter
                if (Monitor.TryEnter(this, TimeSpan.FromSeconds(15)))
                {
                    Monitor.Enter(this);
                    for (int i = 0; i < _iterator; i++)
                    {
                        _message += "A";
                        ThreadInfo.OutputThreadInfo();
                    }
                    WriteLine();
                }
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
    }
}
