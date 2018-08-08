using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XObjectSerializer
{
    internal static class Queries
    {
        internal const string OBJ = @"([0-9]+|[A-Z])(""((`[0-9]+)|(([0-9]+|[A-Z]))(?:('(''|[^'])+')|(""(?:(?=[0-9]+|\0)|[^""])+""))){1,}"")";
        internal const string VAL = @"[0-9A-Z]+'(?:''|[^'])+'";
        internal const string BODY = @"(?<=&[""']).+(?=[""'])";
        internal const string BODYVALID = @"^&(""((`[0-9]+)|(([0-9]+|[A-Z]))(?:('(''|[^'])+')|(""(?:(?=[0-9]+|\0)|[^""])+""))){1,}"")$";
        internal const string PRF = @"^{0}[""']";
        internal const string CENT = @"((?<=[""']).+(?=[""']$))";
        internal const string ANY = @"^{0}[""']";
        internal const string IPNT = @"^`[0-9]";

        internal static string[] RSTRINGS = new string[3] { "`", "'", "\"" };

        internal static string First(this MatchCollection collection, Func<string,bool> func)
        {
            foreach (Match match in collection)
            {
                if (func(match.Value)) return match.Value;
            }
            return null;
        }
        internal static bool Any(this MatchCollection collection, Func<string,bool> func)
        {
            foreach (Match match in collection)
            {
                if (func(match.Value)) return true;
            }
            return false;
        }
        internal static IEnumerable<string> Where(this MatchCollection collection, Func<string, bool> func)
        {
            IList<string> list = new List<string>();
            foreach (Match match in collection)
            {
                if (func(match.Value)) list.Add(match.Value);
            }
            return list;
        }
    }
}
