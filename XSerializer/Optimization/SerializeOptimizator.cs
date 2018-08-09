using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XObjectSerializer.Helpers;

namespace XObjectSerializer.Optimization
{
    internal class SerializeOptimizator : FileOptimizator
    {
        private MatchCollection matches;

        internal SerializeOptimizator(string importFile)
            : base(importFile)
        {
            Regex regex = new Regex(Queries.VAL, RegexOptions.Compiled);
            matches = Regex.Matches(importFile, Queries.VAL, RegexOptions.Compiled);

        }

        //private string Replaces(string value)
        //{

        //}

        internal override string Optimization()
        {


            return null;
        }
    }
}
