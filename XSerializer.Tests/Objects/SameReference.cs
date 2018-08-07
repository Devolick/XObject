using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class SameReference
    {
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public SameReference() {
            Ref1 = "Dzmitry Dym";
            Ref2 = "Dzmitry Dym";
        }
    }
}
