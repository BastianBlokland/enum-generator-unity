using System;

using EnumGenerator.Core;

namespace EnumGenerator.Editor
{
    /// <summary>
    /// ILogger implemention that logs to Unity.
    /// </summary>
    public sealed class UnityLogger : ILogger
    {
        private enum Level
        {
            Trace,
            Debug,
            Information,
            Warning,
            Error,
            Critical
        }

        private readonly bool verbose;
        private readonly UnityEngine.Object context;

        public UnityLogger(bool verbose, UnityEngine.Object context)
        {
            this.verbose = verbose;
            this.context = context;
        }

        public void LogTrace(string message) => this.Log(Level.Trace, message);

        public void LogDebug(string message) => this.Log(Level.Debug, message);

        public void LogInformation(string message) => this.Log(Level.Information, message);

        public void LogWarning(string message) => this.Log(Level.Warning, message);

        public void LogError(string message) => this.Log(Level.Error, message);

        public void LogCritical(string message) => this.Log(Level.Critical, message);

        private void Log(Level level, string message)
        {
            if (!IsEnabled(level))
                return;

            var formattedMessage = $"[EnumGenerator] {level.ToString()}: {message}";
            switch (level)
            {
                case Level.Trace:
                case Level.Debug:
                case Level.Information:
                    UnityEngine.Debug.Log(formattedMessage, context);
                    break;
                case Level.Warning:
                    UnityEngine.Debug.LogWarning(formattedMessage, context);
                    break;
                case Level.Error:
                case Level.Critical:
                    UnityEngine.Debug.LogError(formattedMessage, context);
                    break;
                default:
                    throw new ArgumentException($"Unknown level: '{level}'", nameof(level));
            }
        }

        private bool IsEnabled(Level level)
        {
            switch (level)
            {
                case Level.Trace:
                case Level.Debug: return this.verbose;
                case Level.Information:
                case Level.Warning:
                case Level.Error:
                case Level.Critical: return true;
                default:
                    throw new ArgumentException($"Unknown level: '{level}'", nameof(level));
            }
        }
    }
}
