using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class Stacks: ColletionHelper
    {
        public Stack<bool> Bools { get; set; }
        public Stack<char> Chars { get; set; }
        public Stack<sbyte> SBytes { get; set; }
        public Stack<UInt16> UInt16s { get; set; }
        public Stack<UInt32> UInt32s { get; set; }
        public Stack<UInt64> UInt64s { get; set; }
        public Stack<Int16> Int16s { get; set; }
        public Stack<Int32> Int32s { get; set; }
        public Stack<Int64> Int64s { get; set; }
        public Stack<Double> Doubles { get; set; }
        public Stack<Single> Singles { get; set; }
        public Stack<Decimal> Decimals { get; set; }
        public Stack<string> Strings { get; set; }

        public Stacks() { }
        public Stacks(int size)
        {
            Bools = Adds<bool>(size);
            Chars = Adds<char>(size);
            SBytes = Adds<sbyte>(size);
            UInt16s = Adds<UInt16>(size);
            UInt32s = Adds<UInt32>(size);
            UInt64s = Adds<UInt64>(size);
            Int16s = Adds<Int16>(size);
            Int32s = Adds<Int32>(size);
            Int64s = Adds<Int64>(size);
            Doubles = Adds<Double>(size);
            Singles = Adds<Single>(size);
            Decimals = Adds<Decimal>(size);
            Strings = Adds<string>(size);
        }
        private Stack<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            Stack<T> list = new Stack<T>(size);
            for (int i = 0; i < size; i++)
            {
                list.Push(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
