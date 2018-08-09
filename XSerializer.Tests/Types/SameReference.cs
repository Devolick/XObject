using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class SameReference
    {
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }

        public SameReference() {
            Ref1 = "Dzmitry Dym";
            Ref2 = "Dzmitry";
            Ref3 = "Dzmitry";
            Ref4 = "Dzmitry";
        }
    }
}
