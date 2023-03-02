using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;

namespace BenchmarkLoggerPerf
{
    internal class Program
    {
        // run command: dotnet run -c Release -f net6.0 --filter *LoggerPerf*
        static void Main(string[] args)
            => BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args);
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

        private static ILogger<LoggerPerf> _logger = _loggerFactory.CreateLogger<LoggerPerf>();
        private static LoggerAdapter<LoggerPerf> _loggerAdapter = new LoggerAdapter<LoggerPerf>(_logger);

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
        public void Log_Structure2()
        {
            int nxt = _rand.Next(1_000_000);
            _logger.LogInformation("The next number is {0} at Time:{1}", nxt, DateTime.Now);
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

        [Benchmark]
        public void Log_Structure_Debug_Adapter()
        {
            int nxt = _rand.Next(1_000_000);
           _loggerAdapter.LogDebug("The next number is {Next} at Time:{Time}", nxt, DateTime.Now);
        }
    }
}