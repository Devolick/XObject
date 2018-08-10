using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XObjectSerializer.Helpers
{
    internal static class Queries
    {
        internal const string OBJECT = @"([0-9A-Z]+)(""((`[0-9]+)|(([0-9A-Z]+))(?:('(''|[^'])+')|(""(?:(?=[0-9A-Z]+|\0)|[^""])+""))){1,}"")";
        internal const string VALUE = @"[0-9A-Z]+'(?:''|[^'])+'";
        internal const string BODY = @"(?<=&[""']).+(?=[""'])";
        internal const string VALID = @"^&(""((([0-9A-Z]+))(?:('(''|[^'])+')|(""(?:(?=[0-9A-Z]+|\0)|[^""])+""))){1,}"")$";
        internal const string PREFIX = @"^{0}[""']";
        internal const string GETPREFIX = @"^{0}(?=[""'])";
        internal const string GETDATA = @"((?<=[""']).+(?=[""']$))";
        internal const string INNERPOINTER = @"^`[0-9]$";

        //Old Logic 
        internal static string[] RSTRINGS = new string[4] { "`", "'", "\"", "#" };

        internal static string FirstDefault(this MatchCollection collection, Func<string,bool> func)
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
