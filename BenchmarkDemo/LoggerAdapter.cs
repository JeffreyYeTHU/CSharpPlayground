using Microsoft.Extensions.Logging;

namespace BenchmarkLoggerPerf
{
    public sealed class LoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogDebug<T1>(string template, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogInformation(template, arg1);
            }
        }

        public void LogDebug<T1, T2>(string template, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogInformation(template, arg1, arg2);
            }
        }

        public void LogDebug<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogInformation(template, arg1, arg2, arg3);
            }
        }

        public void LogDebug(string template, object[] args)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogInformation(template, args);
            }
        }
    }
}
