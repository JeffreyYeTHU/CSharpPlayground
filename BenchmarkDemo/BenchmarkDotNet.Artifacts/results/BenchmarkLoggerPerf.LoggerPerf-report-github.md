``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.1265)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2


```
|         Method |     Mean |   Error |   StdDev |   Gen0 | Allocated |
|--------------- |---------:|--------:|---------:|-------:|----------:|
|  Log_Structure | 124.8 ns | 4.00 ns | 11.42 ns | 0.0138 |      88 B |
| Log_Structure2 | 124.0 ns | 4.39 ns | 12.61 ns | 0.0138 |      88 B |
