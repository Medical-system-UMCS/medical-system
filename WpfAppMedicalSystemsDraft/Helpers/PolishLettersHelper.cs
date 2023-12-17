using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMedicalSystemsDraft.Helpers
{
    internal class PolishLettersHelper
    {
        private static Dictionary<string, string> replacements = new Dictionary<string, string>()
        {
            {"ą", "a"}, {"ć", "c"}, {"ę", "e"}, {"ł", "l"},
            {"ń", "n"}, {"ó", "o"}, {"ś", "s"}, {"ż", "z"}, {"ź", "z"},
            {"Ą", "A"}, {"Ć", "C"}, {"Ę", "E"}, {"Ł", "L"},
            {"Ń", "N"}, {"Ó", "O"}, {"Ś", "S"}, {"Ż", "Z"}, {"Ź", "Z"}
        };
        public static string ReplacePolishLetters(string text)
        {
            foreach (var replacement in replacements)
            {
                text = text.Replace(replacement.Key, replacement.Value);
            }

            return text;
        }
    }
}
