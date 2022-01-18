using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace MsTest
{
    class Program
    {
        static ConcurrentDictionary<string, string> memo = new();  // as cache, in practice, can use redis
        static void Main(string[] args)
        {
            var sln = new Solution();
            var testStr = new List<string>(){null, "", "abc", "aab", "aaa", "abcdefedcba", "abcdefedcbaa", "aaaadabccba", "**", "中国中", "//\\", "//\\\\\\"};
            Parallel.ForEach(testStr, str => PrintRes(str, sln));  // parallel processing
        }

        private static void PrintRes(string str, Solution sln){
            if (str != null && memo.ContainsKey(str))
                    Console.WriteLine(memo[str]);
            var res = sln.FindLongestParalindrome(str);
            if (str != null)
                memo.TryAdd(str, res);
            Console.WriteLine(res);
        }
    }
}
