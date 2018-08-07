using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class SortedSets: ColletionHelper
    {
        public SortedSet<bool> Bools { get; set; }
        public SortedSet<char> Chars { get; set; }
        public SortedSet<sbyte> SBytes { get; set; }
        public SortedSet<UInt16> UInt16s { get; set; }
        public SortedSet<UInt32> UInt32s { get; set; }
        public SortedSet<UInt64> UInt64s { get; set; }
        public SortedSet<Int16> Int16s { get; set; }
        public SortedSet<Int32> Int32s { get; set; }
        public SortedSet<Int64> Int64s { get; set; }
        public SortedSet<Double> Doubles { get; set; }
        public SortedSet<Single> Singles { get; set; }
        public SortedSet<Decimal> Decimals { get; set; }
        public SortedSet<string> Strings { get; set; }

        public SortedSets() { }
        public SortedSets(int size)
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
        private SortedSet<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            SortedSet<T> list = new SortedSet<T>();
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
