using System;
using System.Threading;
using System.Collections.Generic;

namespace SimpleThread
{
    class Program
    {
        static Queue<int> queue = new Queue<int>();
        static Thread mainThread;
        static void Main(string[] args)
        {
            mainThread = Thread.CurrentThread;
            ThreadStart childRef = new ThreadStart(ChildThread);
            Thread childThread = new Thread(childRef);
            childThread.Start();


            for (int i = 0; i < 200000; i++)
            {
                if (i % 10000 == 0)
                {
                    queue.Enqueue(i);
                    Console.WriteLine("At " + i + " and child thread is " + childThread.ThreadState.ToString());
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Main Done");
        }

        static void ChildThread()
        {
            Thread.Sleep(2000);
            while (queue.Count > 0 || !mainThread.ThreadState.ToString().Contains("Stopped"))
            {
                Console.WriteLine("\t\t\t\tChild thread At " + queue.Dequeue() + " main thread status is " + mainThread.ThreadState.ToString());
                Thread.Sleep(2000);
            }
            Console.WriteLine("child thread done");
        }
    }
}
    