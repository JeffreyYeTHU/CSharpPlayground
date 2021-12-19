namespace TestBitmap;

internal sealed class Bitmap{
    // Use Byte array to store the status
    private int[] _map;

    // Create a bitmap that can hold maximum of n bits
    public Bitmap(int n){
        int cnt = n / 32 + 1;
        _map = new int[cnt];
    }

    public void SetOne(int k){
        int arrayIdx = k / 32;
        int bitIdx = k % 32;
        int mask = 1 << bitIdx;
        _map[arrayIdx] |= mask;
    }

    public void SetZero(int k){
        int arrayIdx = k / 32;
        int bitIdx = k % 32;
        int mask = ~(1 << bitIdx);
        _map[arrayIdx] &= mask;
    }

    public bool IsTaken(int k){
        int arrayIdx = k / 32;
        int bitIdx = (k % 32);
        int mask = 1 << bitIdx;
        return (_map[arrayIdx] & mask) != 0;
    }
}