using System;
using Microsoft.Extensions.Logging;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// ILogger implemention that logs to Unity.
    /// </summary>
    public sealed class UnityLogger : ILogger
    {
        private readonly bool verbose;
        private readonly UnityEngine.Object context;

        public UnityLogger(bool verbose, UnityEngine.Object context)
        {
            this.verbose = verbose;
            this.context = context;
        }

        public IDisposable BeginScope<T>(T state) => null;

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug: return this.verbose;
                case LogLevel.Information:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical: return true;
                default:
                    throw new ArgumentException($"Unknown level: '{level}'", nameof(level));
            }
        }

        public void Log<T>(
            LogLevel level,
            EventId id,
            T state,
            Exception exception,
            Func<T, Exception, string> formatter)
        {
            if (!IsEnabled(level))
                return;

            var message = $"[EnumGenerator] {formatter(state, exception)}";
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                    UnityEngine.Debug.Log(message, context);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(message, context);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    UnityEngine.Debug.LogError(message, context);
                    break;
                default:
                    throw new ArgumentException($"Unknown level: '{level}'", nameof(level));
            }
        }
    }
}
