using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadSafeQueue;
using System.Threading;

namespace ThreadSafeQueueUnitTest
{
    [TestClass]
    public class CThreadSafeQueueUnitTest
    {
        [TestMethod]
        public void Push_Pop_SimpleTest()
        {
            var q = new CThreadSafeQueue<Int32>();
            q.Push(123);
            q.Push(0);
            
            Assert.AreEqual(q.Pop(), 123, "Wrong value");
            Assert.AreEqual(q.Pop(), 0, "Wrong value");
        }

        [TestMethod]
        [Timeout(3000)]//TestTimeout.Infinite)]
        public void Pop_ShouldWaitForever()
        {
            var q = new CThreadSafeQueue<Int32>();
            q.Pop();            
        }

        [TestMethod]
        [Timeout(600*1000)] //10min
        public void Push_Pop_MultithreadTest()
        {
            CThreadSafeQueue<String> q = new CThreadSafeQueue<String>();
            List<Thread> threads = new List<Thread>();
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                threads.Add(new Thread(() =>
                {
                    for (int j = 0; j < 2000; j++)
                    {
                        Thread.Sleep(rnd.Next(0, 10));//this is a delay for test
                        q.Push("TEST");
                    }
                }) { IsBackground = true });

                threads.Add(new Thread(() =>
                {
                    for (int j = 0; j < 2000; j++)
                    {
                        Assert.AreEqual(q.Pop(), "TEST");
                    }
                }) { IsBackground = true });
            }

            foreach (var t in threads)
                t.Start();

            foreach (var t in threads)
                t.Join();//in this test most important is that threads did not stuck
        }
    }
}
