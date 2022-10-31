/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        private static readonly Random rand = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task<List<int>> first = Task.Run(CreateArrayOfTenIntegers);
            Task<List<int>> second = first.ContinueWith(x => MultiplyWithRandNumber(first.Result));
            Task<List<int>> third = second.ContinueWith(x => SortByAsc(second.Result));
            Task<double> forth = third.ContinueWith(x => CalculateAvg(third.Result));

            Console.ReadLine();
        }

        private static List<int> CreateArrayOfTenIntegers()
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                arr.Add(rand.Next(0, 10));
            }

            PrintArray(arr);
            return arr;
        }

        private static List<int> MultiplyWithRandNumber(List<int> arr)
        {
            int randNum = rand.Next(1, 10);
            Console.WriteLine($"rand num: {randNum}");

            for (int i = 0; i < 10; i++)
            {
                arr[i] *= randNum;
            }

            PrintArray(arr);
            return arr;
        }

        private static List<int> SortByAsc(List<int> arr)
        {
            arr = arr.OrderBy(i => i).ToList();
            PrintArray(arr);

            return arr;
        }

        private static double CalculateAvg(List<int> arr)
        {
            var avg = arr.Average();
            Console.WriteLine($"Avg value: {avg}");

            return avg;
        }

        private static void PrintArray(List<int> list)
        {
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
