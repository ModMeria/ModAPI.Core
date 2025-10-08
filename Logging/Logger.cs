using ModAPI.Abstractions.Logging;

namespace ModAPI.Core.Logging
{
    internal class Logger : ILogger
    {
        private readonly string _modId;
        public Logger(string modId) {
            _modId = modId;
        }

        void ILogger.Debug(string message)
        {
            Allumeria.Logger.Verbose($"{_modId} {message}");
        }

        void ILogger.Error(string message)
        {
            Allumeria.Logger.Error($"{_modId} {message}");
        }

        void ILogger.Info(string message)
        {
            Allumeria.Logger.Info($"{_modId} {message}");
        }

        void ILogger.Warn(string message)
        {
            Allumeria.Logger.Warn($"{_modId} {message}");
        }
    }
}
