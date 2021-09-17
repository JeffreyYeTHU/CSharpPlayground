using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ThreadPoolTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcs = new TaskCompletionSource();
            var tasks = new List<Task>();
            for (int i = 0; i < Environment.ProcessorCount*4; i++)
            {
                int id = i;
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine($"{DateTime.UtcNow:MM:ss.ff}: {id}");
                    tcs.Task.Wait();
                }));
            }
            tasks.Add(Task.Run(() => tcs.SetResult()));

            var sw = Stopwatch.StartNew();
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Done: {sw.Elapsed}");
        }
    }
}
