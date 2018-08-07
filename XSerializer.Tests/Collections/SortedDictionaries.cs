using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class SortedDictionaries: ColletionHelper
    {
        public SortedDictionary<bool, bool> Bools { get; set; }
        public SortedDictionary<char, char> Chars { get; set; }
        public SortedDictionary<sbyte, sbyte> SBytes { get; set; }
        public SortedDictionary<UInt16, UInt16> UInt16s { get; set; }
        public SortedDictionary<UInt32, UInt32> UInt32s { get; set; }
        public SortedDictionary<UInt64, UInt64> UInt64s { get; set; }
        public SortedDictionary<Int16, Int16> Int16s { get; set; }
        public SortedDictionary<Int32, Int32> Int32s { get; set; }
        public SortedDictionary<Int64, Int64> Int64s { get; set; }
        public SortedDictionary<Double, Double> Doubles { get; set; }
        public SortedDictionary<Single, Single> Singles { get; set; }
        public SortedDictionary<Decimal, Decimal> Decimals { get; set; }
        public SortedDictionary<string, string> Strings { get; set; }

        public SortedDictionaries() { }
        public SortedDictionaries(int size)
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
        private SortedDictionary<T,T> Adds<T>(int size) {
            size = CutSize<T>(size);
            SortedDictionary<T,T> list = new SortedDictionary<T,T>();
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i), ForSameKey<T>(i));
            }
            return list;
        }
    }
}
