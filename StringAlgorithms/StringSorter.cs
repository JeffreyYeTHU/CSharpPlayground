namespace StringAlgorithms
{
    public class StringSorter
    {
        // the index is the group the string belongs to
        public static void IndexCountSort(string[] array, int[] index, int maxIdx){
            int[] startIdx = new int[maxIdx + 2];  // 0 is the start idx
            for(int i=0; i<array.Length; i++)
                startIdx[index[i] + 1]++;  // do freq count
            for(int i=1; i<startIdx.Length; i++)
                startIdx[i] += startIdx[i-1];  // accumulate freq to get start idx
            string[] aux = new string[array.Length];
            for(int i=0; i<array.Length; i++){
                aux[startIdx[index[i]]] = array[i];
                startIdx[index[i]]++;
            }
            for(int i=0; i<array.Length; i++)
                array[i] = aux[i];
        }

        public static void LsdSort(string[] array, int w){
            int n = 256;
            int len = array.Length;
            string[] aux = new string[len];
            for(int p = w-1; p>=0; p--){
                int[] startIdx = new int[n+1];
                for(int i=0; i<len; i++){
                    char c = array[i][p];
                    startIdx[c+1]++;
                }
                for(int j=1; j<=n; j++)
                    startIdx[j] += startIdx[j-1];
                for(int i=0; i<len; i++){
                    char c = array[i][p];
                    aux[startIdx[c]] = array[i];
                    startIdx[c]++;
                }
                for(int i=0; i<len; i++)
                    array[i] = aux[i];
            }
        }
    }
}