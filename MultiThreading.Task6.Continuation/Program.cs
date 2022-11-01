/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            Task parentSuccess = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task finishing with success.");
            });

            parentSuccess.ContinueWith(parent =>
            {
                Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            });

            // should not print anything
            parentSuccess.ContinueWith(parent =>
            {
                Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            }, TaskContinuationOptions.OnlyOnFaulted);

            // should not print anything
            parentSuccess.ContinueWith(parent =>
            {
                Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            Task parentException = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task finishing with exception.");
                throw new Exception("Exception thrown from parentException");
            });

            parentException.ContinueWith(parent =>
            {
                Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            });

            parentException.ContinueWith(parent =>
            {
                Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            }, TaskContinuationOptions.OnlyOnFaulted);

            parentException.ContinueWith(parent =>
            {
                Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            var cts = new CancellationTokenSource();
            {
                var token = cts.Token;
                var parentCancelcancelTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Parent task being cancelled.");
                    Task.Delay(2000);
                }, token)
                    .ContinueWith(parent =>
                    {
                        Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
                    }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

                cts.Cancel();
            }

            Task.WaitAll();

            Console.ReadLine();
        }
    }
}
