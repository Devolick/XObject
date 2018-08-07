using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class Queues: ColletionHelper
    {
        public Queue<bool> Bools { get; set; }
        public Queue<char> Chars { get; set; }
        public Queue<sbyte> SBytes { get; set; }
        public Queue<UInt16> UInt16s { get; set; }
        public Queue<UInt32> UInt32s { get; set; }
        public Queue<UInt64> UInt64s { get; set; }
        public Queue<Int16> Int16s { get; set; }
        public Queue<Int32> Int32s { get; set; }
        public Queue<Int64> Int64s { get; set; }
        public Queue<Double> Doubles { get; set; }
        public Queue<Single> Singles { get; set; }
        public Queue<Decimal> Decimals { get; set; }
        public Queue<string> Strings { get; set; }

        public Queues() { }
        public Queues(int size)
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
        private Queue<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            Queue<T> list = new Queue<T>(size);
            for (int i = 0; i < size; i++)
            {
                list.Enqueue(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
