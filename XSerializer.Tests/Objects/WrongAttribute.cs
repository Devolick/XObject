using System;
using System.Collections.Generic;
using System.Text;
using XObjectSerializer.Attributes;

namespace XObjectSerializer.Tests
{
    public class WrongPropertyAttribute
    {
        [XIgnoreProperty]
        public int P1 { get; set; }
        public int P2 { get; set; }

        public WrongPropertyAttribute()
        {
            P1 = 10;
            P2 = 123;
        }
    }
    [XIgnoreClass("P2")]
    public class WrongClassAttribute
    {
        public int P1 { get; set; }
        public int P2 { get; set; }

        public WrongClassAttribute()
        {
            P1 = 10;
            P2 = 123;
        }
    }
}
