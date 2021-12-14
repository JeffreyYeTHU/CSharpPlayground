// See https://aka.ms/new-console-template for more information


var pq = new PriorityQueue<int, int>();
for (int i = 0; i < 10; i++)
{
    int p = Random.Shared.Next(0, 10);
    //int p = 4;
    Console.WriteLine($"enqueue (i,p)=({i},{p})");
    pq.Enqueue(i, p);
}
while (pq.Count > 0)
{
    int e, p;
    pq.TryDequeue(out e, out p);
    Console.WriteLine(e + "," + p);
}
