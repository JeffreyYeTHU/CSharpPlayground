using System;

namespace RecursiveTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[6];
            for(int i=0; i<nums.Length; i++)
                nums[i] = i;
            Postorder(nums, 0);
        }

        static void Preorder(int[] nums, int idx){
            if(idx == nums.Length)
                return;
            Console.WriteLine($"num[{idx}]={nums[idx]}");
            Preorder(nums, idx + 1);
        }

        static void Postorder(int[] nums, int idx){
            if(idx == nums.Length)
                return;
            Postorder(nums, idx + 1);
            Console.WriteLine($"num[{idx}]={nums[idx]}");
        }
    }
}
