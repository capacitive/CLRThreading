using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CLRThreading
{
    partial class Program
    {
        static volatile bool cancel = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Main on thread {0} started. Processor core count: {1}", Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount);

            var message = "Hello from Main() thread!";
            var msg = new Messenger(message);

            var t = new Thread(msg.DisplayMessage)
            {
                Name = "ThreadTest",
                Priority = ThreadPriority.BelowNormal
            };        
            t.Start();

            //method defined in partial class:
            var t1 = new Thread(SayMessage);
            t1.Start();
            var threadId = t1.ManagedThreadId;

            Console.WriteLine("Main on thread {0} done creating threaded tasks. Press enter to end them.", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();

            cancel = true;

            //blocks until thread exits:
            t1.Join();

            Console.WriteLine("SayMessage() thread {0} exited", threadId);
            Console.ReadLine();
        }

        static void RunThread(object stateArg)
        {
            Console.WriteLine("Thread {0} started with message from Main: {1}", Thread.CurrentThread.ManagedThreadId, stateArg);
        }
    }

    public class Messenger
    {
        string _msg;
        public Messenger(string msg)
        {
            Console.WriteLine("Messenger thread {0} started.", Thread.CurrentThread.ManagedThreadId);
            _msg = msg;
        }

        public void DisplayMessage()
        {
            Console.WriteLine(_msg + " Sent to thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }  
}
