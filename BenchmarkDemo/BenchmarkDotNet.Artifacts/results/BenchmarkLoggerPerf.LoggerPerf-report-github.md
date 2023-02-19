``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.1265)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2


```
|                      Method |      Mean |    Error |    StdDev |   Gen0 | Allocated |
|---------------------------- |----------:|---------:|----------:|-------:|----------:|
|            Log_StringInterp | 303.96 ns | 6.218 ns | 18.335 ns | 0.0215 |     135 B |
|               Log_Structure | 103.69 ns | 2.058 ns |  5.048 ns | 0.0139 |      88 B |
|         Log_Structure_Debug | 106.62 ns | 2.117 ns |  5.193 ns | 0.0139 |      88 B |
|  Log_Structure_Debug_WithIf |  17.33 ns | 0.691 ns |  2.036 ns |      - |         - |
| Log_Structure_Debug_Adapter |  46.84 ns | 1.205 ns |  3.398 ns |      - |         - |
