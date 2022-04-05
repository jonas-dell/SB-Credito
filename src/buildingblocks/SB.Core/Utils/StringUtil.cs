using System.Linq;

namespace SB.Core.Utils
{
    public static class StringUtil
    {
        public static string OnlyNumbers(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
