/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static int counter = 1;
        private static readonly object lockObj = new object();
        private static readonly Semaphore semaphore = new Semaphore(0, 1);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Do work using Thread class and Join for waiting.");
            DoWork(20);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Do work using ThreadPool class and Semaphore for waiting.");
            counter = 1;
            DoWorkWithThreadPool(10);
            semaphore.WaitOne(1);
            
            Console.ReadLine();
        }


        private static void DoWork(object state)
        {
            int num = (int)state;
            num--;

            Console.WriteLine($"State in thread #{counter} is {num}");

            if (++counter > 10)
            {
                return;
            }

            Thread thread = new Thread(new ParameterizedThreadStart(DoWork));
            thread.Start(num);
            thread.Join();
        }

        private static void DoWorkWithThreadPool(object state)
        {
            int num = (int)state;
            num--;

            Console.WriteLine($"State in thread #{counter} is {num}");
            
            if (++counter > 10)
            {
                semaphore.Release();
                return;
            }

            ThreadPool.QueueUserWorkItem(DoWorkWithThreadPool, num);
        }
    }
}
