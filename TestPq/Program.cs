using System;
using System.Collections.Generic;

namespace TestPq
{
    class Program
    {
        static void Main(string[] args)
        {
            var pq = new PriorityQueue<int, int>();
            for(int i=0; i<10; i++)
            {
                pq.Enqueue(i, 10-i);
            }
            while(pq.Count > 0)
            {
                Console.WriteLine(pq.Dequeue());
            }
        }
    }
}
