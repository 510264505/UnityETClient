using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ETHotfix
{
    public static class RegexHelper
    {
        public static string RegexMatch(string pattern, string html)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(html);
            return match.Groups["value"].Value;
        }
        public static int[] RegexIntMatches(string pattern, string html)
        {
            List<int> list = new List<int>();
            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(html);
            foreach (Match item in match)
            {
                list.Add(int.Parse(item.Groups["value"].Value));
            }
            return list.ToArray();
        }
        public static float[] RegexFloatMatches(string pattern, string html)
        {
            List<float> list = new List<float>();
            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(html);
            foreach (Match item in match)
            {
                list.Add(float.Parse(item.Groups["value"].Value));
            }
            return list.ToArray();
        }
        public static string[] RegexStringMatches(string pattern, string html)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(html);
            foreach (Match item in match)
            {
                list.Add(item.Groups["value"].Value.Trim());
            }
            return list.ToArray();
        }
    }
}
