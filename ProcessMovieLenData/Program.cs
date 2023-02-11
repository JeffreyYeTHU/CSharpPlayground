using System;
using System.IO;

var oldFileName = "/Users/jeffrey/Downloads/ml-10M100K/ratings.dat";
var newFileName = "/Users/jeffrey/Downloads/ml-10M100K/ratings_100k.dat";
var lines = File.ReadLines(oldFileName);
using StreamWriter writer = File.AppendText(newFileName);
int cnt = 0;
foreach (var line in lines)
{
    cnt++; 
    var newLine = line.Replace("::", "$");
    await writer.WriteLineAsync(newLine);
    if (cnt > 100_000) break;
}