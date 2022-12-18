using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var nums = new int[] { 1, 3, -1, -3, 5, 3, 6, 7 };
            //var max = MaxSlidingWindow(nums, 3);

            //string s = "abcabcbb";
            //int maxLen = LengthOfLongestSubstring(s);

            //DateTime? dateTime = null;
            //Console.WriteLine(String.Empty == dateTime.ToString());
            //Console.WriteLine($"datetime:{dateTime}");

            //var timeF = DateTime.UtcNow.ToString("yyyy-MM-dd_HH:mm:ss", CultureInfo.GetCultureInfo("en-US"));
            //Console.WriteLine(timeF);

            var res = Regex.Match("TimerTrigger_2022-08-21_03:15:19822aba45-9dd5-4cc6-9d8e-d22e5166a5fb", "-[0-9a-z]+$", RegexOptions.IgnoreCase);
            Console.WriteLine(res);
        }

        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            int len = nums.Length;
            int[] res = new int[len - k + 1];
            int i = 0;
            List<int> descQueue = new();
            while (i < k)
                Enqueue(descQueue, nums[i]);

            Console.WriteLine($"i={i}");
            for (int j = 0; j < descQueue.Count; j++)
                Console.WriteLine($"dq[{j}]={descQueue[j]}");

            res[0] = descQueue[0];
            for (; i < len; i++)
            {
                if (nums[i - k] == descQueue[0])
                    descQueue.RemoveAt(0);
                Enqueue(descQueue, nums[i]);
                res[i - k + 1] = descQueue[0];
            }
            return res;
        }

        private static void Enqueue(List<int> q, int v)
        {
            if (q.Count == 0)
            {
                q.Add(v);
                return;
            }
            int i = q.Count - 1;
            while (i >= 0 && q[i] < v)
                i--;
            if (i < q.Count - 1)
                q.RemoveRange(i + 1, q.Count - i - 1);
            q.Add(v);
        }

        public static int LengthOfLongestSubstring(string s)
        {
            if (s == null)
                return 0;
            int left = 0, right = 0;
            var map = new Dictionary<char, int>();
            int start = 0;
            int maxLen = 0;
            while (right < s.Length)
            {
                // enlarge window 
                if (!map.ContainsKey(s[right]))
                    map.Add(s[right], 0);
                map[s[right]]++;
                right++;

                // shink window
                while (map[s[right]] > 1)
                {
                    map[s[left]]--;
                    left++;
                }

                // update res
                if (right - left + 1 > maxLen)
                {
                    start = left;
                    maxLen = right - left + 1;
                }
            }
            return maxLen;
        }
    }
}