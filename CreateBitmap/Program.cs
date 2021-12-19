using TestBitmap;

var bm = new Bitmap(10000);
bm.SetOne(3);
bm.SetOne(30);
bm.SetOne(300);
bm.SetOne(3000);

Console.WriteLine($"is 3000 set: {bm.IsTaken(3000)}");
Console.WriteLine($"is 45 set: {bm.IsTaken(45)}");
Console.WriteLine($"is 450 set: {bm.IsTaken(450)}");