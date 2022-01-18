using System.Linq;

public class Solution{
    
    public string FindLongestParalindrome(string s){
        if (string.IsNullOrEmpty(s))
            return s;
        int len = s.Length;
        var codePonts = s.EnumerateRunes().Count();
        var sarr = s.ToCharArray();  // handle special char internally
        int resLeft = 0;
        int resRight = 0;
        int resLen = resRight - resLeft;
        for(int i=0; i<s.Length; i++){
            var (left1, right1) = ExpandFromCenter(sarr, i, i);
            var (left2, right2) = ExpandFromCenter(sarr, i, i+1);
            if (right1 - left1 > right2 - left2 && right1-left1 > resLen){
                // update res
                resLeft = left1;
                resRight = right1;
                resLen = resRight - resLeft;
            }
            else if (right2 - left2 >= right1 - left1 && right2-left2 > resLen){
                resLeft = left2;
                resRight = right2;
                resLen = resRight - resLeft;
            }
        }
        return s.Substring(resLeft, resLen+1);
    }

    private (int left, int right) ExpandFromCenter(char[] s, int p, int q){
        while(p >=0 && q < s.Length && s[p] == s[q]){
            p--;
            q++;
        }
        return (p+1,q-1);
    }
}