using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace BenchmarkDemo
{
    internal class Program
    {
        static void Main(string[] args)
            => BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args);
    }

    [MemoryDiagnoser]
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }

    [MemoryDiagnoser]
    public class LoggerPerf
    {
        private static ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Default", LogLevel.Information);
                //.AddConsole();
        });

        private static ILogger _logger = _loggerFactory.CreateLogger(nameof(LoggerPerf));

        private static Random _rand = new Random(0);

        [Benchmark]
        public void Log_StringInterp()
        {
            int nxt = _rand.Next(1_000_000);
            _logger.LogInformation($"The next number is {nxt} at Time:{DateTime.Now}");
        }

        [Benchmark]
        public void Log_Structure()
        {
            int nxt = _rand.Next(1_000_000);
            _logger.LogInformation("The next number is {Next} at Time:{Time}", nxt, DateTime.Now);
        }

        [Benchmark]
        public void Log_StringInterp_Debug()
        {
            int nxt = _rand.Next(1_000_000);
            _logger.LogDebug($"The next number is {nxt} at Time:{DateTime.Now}");
        }

        [Benchmark]
        public void Log_Structure_Debug()
        {
            int nxt = _rand.Next(1_000_000);
            _logger.LogDebug("The next number is {Next} at Time:{Time}", nxt, DateTime.Now);
        }

        [Benchmark]
        public void Log_Structure_Debug_WithIf()
        {
            int nxt = _rand.Next(1_000_000);
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("The next number is {Next} at Time:{Time}", nxt, DateTime.Now);
            }
        }
    }
}