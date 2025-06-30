using ModAPI.Abstractions.Logging;

namespace ModAPI.Core.Logging
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _modName;
        private readonly TextWriter _defaultOut = Console.Out;

        public ConsoleLogger(string modName)
        {
            _modName = modName;
        }
        
        private void Log(string level, ConsoleColor color, string message)
        {
            TextWriter writer = Console.Out;
            Console.SetOut(_defaultOut);
            string currentTime = DateTime.Now.ToString("HH:mm:ss.fff");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{currentTime}] ");

            Console.ForegroundColor = color;
            Console.WriteLine($"[{level}]  [{_modName}]    {message}");

            Console.ResetColor();
            Console.SetOut(writer);
        }
        
        public void Info(string message)  => Log("INFO", ConsoleColor.White, message);
        public void Warn(string message)  => Log("WARN", ConsoleColor.Yellow, message);
        public void Error(string message) => Log("ERROR", ConsoleColor.Red, message);
        public void Debug(string message) => Log("DEBUG", ConsoleColor.Green, message);
        
    }   
}