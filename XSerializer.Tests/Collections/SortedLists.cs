using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class SortedLists: ColletionHelper
    {
        public SortedList<bool, bool> Bools { get; set; }
        public SortedList<char, char> Chars { get; set; }
        public SortedList<sbyte, sbyte> SBytes { get; set; }
        public SortedList<UInt16, UInt16> UInt16s { get; set; }
        public SortedList<UInt32, UInt32> UInt32s { get; set; }
        public SortedList<UInt64, UInt64> UInt64s { get; set; }
        public SortedList<Int16, Int16> Int16s { get; set; }
        public SortedList<Int32, Int32> Int32s { get; set; }
        public SortedList<Int64, Int64> Int64s { get; set; }
        public SortedList<Double, Double> Doubles { get; set; }
        public SortedList<Single, Single> Singles { get; set; }
        public SortedList<Decimal, Decimal> Decimals { get; set; }
        public SortedList<string, string> Strings { get; set; }

        public SortedLists() { }
        public SortedLists(int size)
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
        private SortedList<T,T> Adds<T>(int size) {
            size = CutSize<T>(size);
            SortedList<T,T> list = new SortedList<T,T>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i), ForSameKey<T>(i));
            }
            return list;
        }
    }
}
