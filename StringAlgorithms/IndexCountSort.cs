namespace StringAlgorithms
{
    public class IndexCountSort
    {
        // the index is the group the string belongs to
        public static void Sort(string[] array, int[] index, int maxIdx){
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
    }
}