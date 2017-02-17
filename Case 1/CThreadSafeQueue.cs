using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSafeQueue
{
    //Задание 1.Надо сделать очередь с операциями push(T) и T pop().
    //Операции должны поддерживать обращение с разных потоков.
    //Операция push всегда вставляет и выходит.
    //Операция pop ждет пока не появится новый элемент.
    //В качестве контейнера внутри можно использовать только стандартную очередь (Queue).
    class CThreadSafeQueue<T>
    {
        private readonly Object _locker;
        private readonly Queue<T> _queue;
        private readonly ManualResetEvent _hasElementsEvent;

        public CThreadSafeQueue()
        {
            _locker = new Object();
            _queue = new Queue<T>();
            _hasElementsEvent = new ManualResetEvent(false);
        }

        public void Push(T obj)
        {
            lock (_locker)
            {
                _queue.Enqueue(obj);
                _hasElementsEvent.Set();
            }
        }

        public T Pop()
        {
            while (true)
            {
                _hasElementsEvent.WaitOne();//this event can be deleted, it just helps not to run many cycles of while(true), reduces app. performance, but saves CPU time

                lock (_locker)
                {
                    if (!_queue.Any())                    
                        _hasElementsEvent.Reset();                    
                    else
                    {
                        if (_queue.Count == 1)                        
                            _hasElementsEvent.Reset();
                        
                        return _queue.Dequeue();
                    }
                }
            }
        }
    }
}
