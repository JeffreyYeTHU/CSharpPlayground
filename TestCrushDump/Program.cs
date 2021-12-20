// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

await Task.Delay(TimeSpan.FromSeconds(5));
throw new Exception("Test crush dump");