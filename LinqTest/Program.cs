using System;
using System.Linq;

namespace LinqTest
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine ("Hello World!");
            var s = new int[] { 1, 5, 3};
            var sortedS = s.OrderByDescending(v => v).ToList();
            foreach (var item in s)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine();
            foreach (var item in sortedS)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine();
        }
    }
}