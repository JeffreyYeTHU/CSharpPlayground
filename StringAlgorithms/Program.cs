using System;

namespace StringAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            // IndexCountSortTest();
            LsdSortTest();
        }

        private static void IndexCountSortTest()
        {
            string[] arr = new string[] { "jeff", "leon", "phillip", "john", "paul", "zander" };
            int[] index = new int[] { 1, 2, 0, 1, 0, 0 };
            int maxIdx = 2;
            StringSorter.IndexCountSort(arr, index, maxIdx);
        }

        private static void LsdSortTest(){
            string[] arr = new string[]{"4PGC9", "2IYE2", "3CIO7", "1ICK3", "4JZY1", "3F8V4",
             "1B2OC", "2J6Z3", "1M1BH", "3CIO7", "1ICK3", "2J6Z3", "2IYE2", "2WPT1", "4JZY1", 
             "1M1BH", "1B2OC", "3F8V4", "4PGC9", "2WPT1"};
            // string[] arr = new string[]{"4PGC9", "2IYE2", "3CIO7", "1ICK3", "4JZY1", "3F8V4"};
            int w = 5;
            StringSorter.LsdSort(arr, w);
        }
    }
}
