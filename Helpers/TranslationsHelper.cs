using Allumeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModAPI.Core.Helpers
{
    internal static class TranslationsHelper
    {
        public static Dictionary<string, string> ParseKeys(string path)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var rawLine in File.ReadAllLines(path))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrEmpty(line)) continue;

                var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    dict[parts[0]] = parts[1];
                }
                else {
                    Logger.Warn($"[ModMeria] Malformed translation line in {Path.GetFileName(path)}: '{line}'");
                }
            }

            return dict;
        }
    }
}
