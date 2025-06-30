using System.Text;

namespace ModAPI.Logging
{
    public class LoggerTextWriter : TextWriter
    {
        private readonly ConsoleLogger _logger = new ConsoleLogger("Allumeria");

        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _logger.Info($"{value}");
            }
        }

        public override void Write(string? value)
        {
            if (!string.IsNullOrEmpty(value) && value.EndsWith('\n'))
            {
                _logger.Info($"{value.TrimEnd()}");
            }
        }
    }
}