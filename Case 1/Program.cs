using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest();
            Console.ReadLine();
        }

        private static void RunTest()
        {
            CThreadSafeQueue<String> q = new CThreadSafeQueue<String>();
            List<Thread> threads = new List<Thread>();
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Thread(() =>
                {
                    Thread.Sleep(rnd.Next(0, 100));//this is a delay for test
                    Console.WriteLine("Pushing strings into queue");
                    q.Push("TEST1");
                    q.Push("TEST2");
                }) { IsBackground = true });

                threads.Add(new Thread(() =>
                {
                    Console.WriteLine("Got from queue:{0}", q.Pop());
                    Console.WriteLine("Got from queue:{0}", q.Pop());//if queue is empty these threads will not end
                }) { IsBackground = true });
            }

            foreach (var t in threads)
                t.Start();
            Console.WriteLine("all threads started");

            foreach (var t in threads)
                t.Join();
            Console.WriteLine("all threads stopped");
        }
    }
}
