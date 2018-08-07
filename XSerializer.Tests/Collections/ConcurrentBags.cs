using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class ConcurrentBags: ColletionHelper
    {
        public ConcurrentBag<bool> Bools { get; set; }
        public ConcurrentBag<char> Chars { get; set; }
        public ConcurrentBag<sbyte> SBytes { get; set; }
        public ConcurrentBag<UInt16> UInt16s { get; set; }
        public ConcurrentBag<UInt32> UInt32s { get; set; }
        public ConcurrentBag<UInt64> UInt64s { get; set; }
        public ConcurrentBag<Int16> Int16s { get; set; }
        public ConcurrentBag<Int32> Int32s { get; set; }
        public ConcurrentBag<Int64> Int64s { get; set; }
        public ConcurrentBag<Double> Doubles { get; set; }
        public ConcurrentBag<Single> Singles { get; set; }
        public ConcurrentBag<Decimal> Decimals { get; set; }
        public ConcurrentBag<string> Strings { get; set; }

        public ConcurrentBags() { }
        public ConcurrentBags(int size)
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
        private ConcurrentBag<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            ConcurrentBag<T> list = new ConcurrentBag<T>();
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
