using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class Types
    {
        public bool Bool { get; set; }
        public char Char { get; set; }
        public sbyte SByte { get; set; }
        public UInt16 UInt16 { get; set; }
        public UInt32 UInt32 { get; set; }
        public UInt64 UInt64 { get; set; }
        public Int16 Int16 { get; set; }
        public Int32 Int32 { get; set; }
        public Int64 Int64 { get; set; }
        public Double Double { get; set; }
        public Single Single { get; set; }
        public Decimal Decimal { get; set; }
        public string String { get; set; }

        public Types() {
            Bool = true;
            Char = 'A';
            SByte = 15;
            UInt16 = 10;
            UInt32 = 10;
            UInt64 = 10;
            Int16 = 10;
            Int32 = 10;
            Int64 = 10;
            Double = 10;
            Single = 10;
            Decimal = 10;
            String = "say hello";
        }
    }
}
