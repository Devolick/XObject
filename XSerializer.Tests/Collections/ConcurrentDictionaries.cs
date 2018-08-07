using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class ConcurrentDictionaries:ColletionHelper
    {
        public ConcurrentDictionary<bool, bool> Bools { get; set; }
        public ConcurrentDictionary<char, char> Chars { get; set; }
        public ConcurrentDictionary<sbyte, sbyte> SBytes { get; set; }
        public ConcurrentDictionary<UInt16, UInt16> UInt16s { get; set; }
        public ConcurrentDictionary<UInt32, UInt32> UInt32s { get; set; }
        public ConcurrentDictionary<UInt64, UInt64> UInt64s { get; set; }
        public ConcurrentDictionary<Int16, Int16> Int16s { get; set; }
        public ConcurrentDictionary<Int32, Int32> Int32s { get; set; }
        public ConcurrentDictionary<Int64, Int64> Int64s { get; set; }
        public ConcurrentDictionary<Double, Double> Doubles { get; set; }
        public ConcurrentDictionary<Single, Single> Singles { get; set; }
        public ConcurrentDictionary<Decimal, Decimal> Decimals { get; set; }
        public ConcurrentDictionary<string, string> Strings { get; set; }

        public ConcurrentDictionaries() { }
        public ConcurrentDictionaries(int size)
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
        private ConcurrentDictionary<T,T> Adds<T>(int size) {
            size = CutSize<T>(size);
            ConcurrentDictionary<T,T> list = new ConcurrentDictionary<T,T>();
            for (int i = 0; i < size; i++)
            {
                var x = ForSameKey<T>(i);
                list.AddOrUpdate(ForSameKey<T>(i), ForSameKey<T>(i), (key, oldValue) => ForSameKey<T>(i));
            }
            return list;
        }
    }
}
