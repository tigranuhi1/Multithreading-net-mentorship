/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static readonly EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private static bool isWriteCompleted = false;
        private static IList<int> col = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Task writer = Task.Factory.StartNew(Write);
            Task reader = Task.Factory.StartNew(Read);

            Task.WaitAll(reader, writer);
            Console.ReadLine();
        }

        static void Read()
        {
            
            while (!isWriteCompleted)
            {
                waitHandle.WaitOne();
                for (int i = 0; i < col.Count; i++)
                {
                    Console.Write(i + " ");
                    
                }
                Console.WriteLine();
            }
        }

        static void Write()
        {
            for (int i = 0; i < 10; i++)
            {                
                col.Add(i);
                waitHandle.Set();
                Thread.Sleep(2000);
            }

            isWriteCompleted = true;
        }
    }
}
