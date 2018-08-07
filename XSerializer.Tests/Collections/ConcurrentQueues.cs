using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class ConcurrentQueues: ColletionHelper
    {
        public ConcurrentQueue<bool> Bools { get; set; }
        public ConcurrentQueue<char> Chars { get; set; }
        public ConcurrentQueue<sbyte> SBytes { get; set; }
        public ConcurrentQueue<UInt16> UInt16s { get; set; }
        public ConcurrentQueue<UInt32> UInt32s { get; set; }
        public ConcurrentQueue<UInt64> UInt64s { get; set; }
        public ConcurrentQueue<Int16> Int16s { get; set; }
        public ConcurrentQueue<Int32> Int32s { get; set; }
        public ConcurrentQueue<Int64> Int64s { get; set; }
        public ConcurrentQueue<Double> Doubles { get; set; }
        public ConcurrentQueue<Single> Singles { get; set; }
        public ConcurrentQueue<Decimal> Decimals { get; set; }
        public ConcurrentQueue<string> Strings { get; set; }

        public ConcurrentQueues() { }
        public ConcurrentQueues(int size)
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
        private ConcurrentQueue<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            ConcurrentQueue<T> list = new ConcurrentQueue<T>();
            for (int i = 0; i < size; i++)
            {
                list.Enqueue(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
