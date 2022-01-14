using System;
using System.Collections.Generic;

namespace MsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
        }

        static bool IsWordExist(List<String> matrix, string word){
            // find possible start
            var start = new List<(int row, int col)>();
            m = matrix.Count;
            n = matrix[0].Length;
            visited = new bool[m,n];
            for(int i=0; i<m; i++){
                for(int j=0; j<n; j++){
                    if (matrix[i][j] == word[0]){
                        start.Add((i, j));
                    }
                }
            }

            // do dfs
            foreach(var (r, c) in start){
                // visited = new bool[m,n];
                if (!exist)
                    Dfs(matrix, r, c, word, 0);
            }

            return exist;
        }

        // for matrix, find word[idx]
        static bool exist = false;
        static int m;
        static int n;
        static bool[,] visited;
        static void Dfs(List<string> matrix, int row, int col, string word, int idx){
            if (exist)
                return;
            else if(idx >= word.Length){
                exist = true;
                return;
            }
            else if (row < 0 || row >=m || col <0 || col >=n || visited[row,col] || matrix[row][col] != word[idx]){
                return;
            }
            
            // backtrack
            visited[row, col] = true;  // make choice
            Dfs(matrix, row+1, col, word, idx+1);
            Dfs(matrix, row-1, col, word, idx+1);
            Dfs(matrix, row, col+1, word, idx+1);
            Dfs(matrix, row, col-1, word, idx+1);
            visited[row, col] = false;  // undo choice
        }

        static Dictionary<ListNode, ListNode> originalToCopy = new();
        public static ListNode DeepCopy(ListNode root){
            var p = root;
            while(p != null){
                // for current
                ListNode copy;
                if (!originalToCopy.ContainsKey(p)){
                    copy = MakeCopy(p);
                    originalToCopy.Add(p, copy);
                }
                else{
                    copy = originalToCopy[p];
                }

                // for next
                if (!originalToCopy.ContainsKey(p.Next)){
                    copy.Next = MakeCopy(p.Next);
                    originalToCopy.Add(p.Next, copy.Next);
                }
                else
                    copy.Next = originalToCopy[p.Next];

                // for random
                if (!originalToCopy.ContainsKey(p.Random)){
                    copy.Random = MakeCopy(p.Random);
                    originalToCopy.Add(p.Random, copy.Random);
                }
                else
                    copy.Random = originalToCopy[p.Random];

                p = p.Next;
            }
            return originalToCopy[root];
        }

        private static ListNode MakeCopy(ListNode node){
            if (node == null)
                return null;
            var copy = new ListNode(node.Val);
            return copy;
        }
    }

    public class ListNode{
        public ListNode Next{get;set;}
        public ListNode Random{get;set;}
        public int Val{get; set;}

        public ListNode(int val, ListNode next = null, ListNode random = null){
            Val = val;
            Next = next;
            Random = random;
        }
    }
}
