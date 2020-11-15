using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace WeETL
{
    public enum StringRandomStyle
    {
        LowerCase,
        UpperCase,
        Mixed
    }
    public sealed class ETLString
    {
        private static Random randomizer = new Random();

        public static bool GetBooleanRandom() => randomizer.Next(0, 2) > 0;
        public static string GetToto() => GetAsciiRandomString();
        public static string GetAsciiRandomString(int length = 6, StringRandomStyle style = StringRandomStyle.Mixed)
        {

            StringBuilder sb = new StringBuilder();
            while (sb.Length <= length)
                sb.Append(
                 style switch
                 {
                     StringRandomStyle.LowerCase => ((char)randomizer.Next('a', 'z')).ToString(),
                     StringRandomStyle.UpperCase => ((char)randomizer.Next('A', 'Z')).ToString(),
                     StringRandomStyle.Mixed => GetBooleanRandom() ? ((char)randomizer.Next('a', 'z')).ToString() : ((char)randomizer.Next('A', 'Z')).ToString(),
                     _ => string.Empty
                 });
            return sb.ToString();
        }
        public static int GetIntRandom(int from, int to) => randomizer.Next(from, to);

       
    }
}
