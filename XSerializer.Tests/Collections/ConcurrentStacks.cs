using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class ConcurrentStacks: ColletionHelper
    {
        public ConcurrentStack<bool> Bools { get; set; }
        public ConcurrentStack<char> Chars { get; set; }
        public ConcurrentStack<sbyte> SBytes { get; set; }
        public ConcurrentStack<UInt16> UInt16s { get; set; }
        public ConcurrentStack<UInt32> UInt32s { get; set; }
        public ConcurrentStack<UInt64> UInt64s { get; set; }
        public ConcurrentStack<Int16> Int16s { get; set; }
        public ConcurrentStack<Int32> Int32s { get; set; }
        public ConcurrentStack<Int64> Int64s { get; set; }
        public ConcurrentStack<Double> Doubles { get; set; }
        public ConcurrentStack<Single> Singles { get; set; }
        public ConcurrentStack<Decimal> Decimals { get; set; }
        public ConcurrentStack<string> Strings { get; set; }

        public ConcurrentStacks() { }
        public ConcurrentStacks(int size)
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
        private ConcurrentStack<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            ConcurrentStack<T> list = new ConcurrentStack<T>();
            for (int i = 0; i < size; i++)
            {
                list.Push(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
