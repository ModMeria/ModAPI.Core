using System.Text;

namespace ModAPI.Core.Logging
{
    public class LoggerTextWriter : TextWriter
    {
        private readonly ConsoleLogger _logger = new ConsoleLogger("Allumeria");
        
        private readonly TextWriter _defaultOut = Console.Out;
        
        public override Encoding Encoding => Encoding.UTF8;
        
        public override void WriteLine(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Console.SetOut(_defaultOut);
                
                _logger.Info($"{value}");
                
                Console.SetOut(this);
            }
        }
        
        public override void Write(string? value)
        {
            if (!string.IsNullOrEmpty(value) && value.EndsWith('\n'))
            {
                Console.SetOut(_defaultOut);
                
                _logger.Info($"{value.TrimEnd()}");
                
                Console.SetOut(this);
            }
        }
    }
}