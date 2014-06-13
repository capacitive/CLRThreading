using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CLRThreading
{
    partial class Program
    {
        static void SayMessage()
        {
            while(!cancel)
            {
                Console.WriteLine("Message from thread {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
        }
    }
}
