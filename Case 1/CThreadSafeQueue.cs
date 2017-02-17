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
        private readonly ManualResetEvent _notEmptyEvent;

        public CThreadSafeQueue()
        {
            _locker = new Object();
            _queue = new Queue<T>();
            _notEmptyEvent = new ManualResetEvent(false);
        }

        public void Push(T obj)
        {
            lock (_locker)
            {
                _queue.Enqueue(obj);
                _notEmptyEvent.Set();
            }
        }

        public T Pop()
        {
            while (true)
            {
                _notEmptyEvent.WaitOne();//this event can be deleted, it just helps not to run many cycles of while(true), reduces app. performance, but saves CPU time

                lock (_locker)
                {
                    if (!_queue.Any())                    
                        _notEmptyEvent.Reset();                    
                    else
                    {
                        if (_queue.Count == 1)                        
                            _notEmptyEvent.Reset();
                        
                        return _queue.Dequeue();
                    }
                }
            }
        }
    }
}
