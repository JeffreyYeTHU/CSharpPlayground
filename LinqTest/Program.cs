using System;
using System.Linq;

namespace LinqTest
{
    class Program
    {
        static void Main (string[] args)
        {
            //Console.WriteLine ("Hello World!");
            //var s = new int[] { 1, 5, 3};
            //var sortedS = s.OrderByDescending(v => v).ToList();
            //foreach (var item in s)
            //{
            //    Console.Write(item + ",");
            //}
            //Console.WriteLine();
            //foreach (var item in sortedS)
            //{
            //    Console.Write(item + ",");
            //}
            //Console.WriteLine();

            DicesProbability(2);
        }

        public static double[] DicesProbability(int n)
        {
            double[] dp = new double[5 * n + 1];
            for (int i = 0; i < 6; i++)
                dp[i] = 1 / 6.0d;
            for (int i = 2; i <= n; i++)
            {
                int preLen = 5 * (i - 1) + 1;
                double[] temp = new double[preLen];
                for (int j = 0; j < preLen; j++)
                {
                    temp[j] = dp[j];
                    dp[j] = 0;
                }
                for (int j = 0; j < preLen; j++)
                {
                    for (int k = j; k < j + 6; k++)
                    {
                        dp[k] += temp[j] / 6.0d;
                    }
                }
            }
            return dp;
        }
    }
}