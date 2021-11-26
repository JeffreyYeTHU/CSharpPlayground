int[,] data = new int[,] { 
    {1, 2, 3, 4, 5},
    {5, 6, 7, 8, 9},
    {9, 10, 11, 12, 13},
    {13, 14, 15, 16, 17}};

// normal
int m = data.GetLength(0);
int n = data.GetLength(1);
//for (int i = 0; i < data.GetLength(0); i++)
//{
//    Console.WriteLine();
//    for (int j = 0; j < data.GetLength(1); j++)
//    {
//        Console.Write(data[i, j] + ", ");
//    }
//}

//// zigzag
//bool leftToRight = true;
//for (int i = 0; i < data.GetLength(0); i++)
//{
//    Console.WriteLine();
//    if (leftToRight)
//    {
//        for (int j = 0; j < data.GetLength(1); j++)
//        {
//            Console.Write(data[i, j] + ", ");
//        }
//        leftToRight = !leftToRight;
//    }
//    else
//    {
//        for (int j = data.GetLength(1) -1; j >=0 ; j--)
//        {
//            Console.Write(data[i, j] + ", ");
//        }
//        leftToRight = !leftToRight;
//    }
//}

//// diagnal
//int cnt = 1;
//int outer = m + n - 1;
//int startR = m - 1;
//int startC = 0;
//while (cnt <= outer)
//{
//    Console.WriteLine();
//    int i = startR;
//    int j = startC;
//    while (i >= 0 && i < m && j >=0 && j < n)
//    {
//        Console.Write(data[i, j] + ", ");
//        i++;
//        j++;
//    }
//    startR = cnt < m ? startR - 1 : 0;
//    startC = cnt < m ? 0 : startC + 1;
//    cnt++;
//}

// circle
int total = m * n;
int cnt = 0;
int i = -1, j = -1;
while(cnt < total)
{
    //Console.WriteLine();
    i++;
    j++;
    while(j < n && data[i, j] != -1)
    {
        Console.WriteLine(data[i, j] + ", ");
        data[i, j] = -1;
        cnt++;
        j++;
    }
    i++;
    j--;
    while(i < m && data[i, j] != -1)
    {
        Console.WriteLine(data[i, j] + ", ");
        data[i, j] = -1;
        cnt++;
        i++;
    }
    j--;
    i--;
    while(j >=0 && data[i, j] != -1)
    {
        Console.WriteLine(data[i, j] + ", ");
        data[i, j] = -1;
        cnt++;
        j--;
    }
    i--;
    j++;
    while(i >=0 && data[i, j] != -1)
    {
        Console.WriteLine(data[i, j] + ", ");
        data[i, j] = -1;
        cnt++;
        i--;
    }
}