using System;

namespace StringAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = new string[]{"jeff", "leon", "phillip", "john", "paul", "zander"};
            int[] index = new int[]{1, 2, 0, 1, 0, 0};
            int maxIdx = 2;
            IndexCountSort.Sort(arr, index, maxIdx);
        }
    }
}
