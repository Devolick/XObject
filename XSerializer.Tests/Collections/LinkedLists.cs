using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class LinkedLists: ColletionHelper
    {
        public LinkedList<bool> Bools { get; set; }
        public LinkedList<char> Chars { get; set; }
        public LinkedList<sbyte> SBytes { get; set; }
        public LinkedList<UInt16> UInt16s { get; set; }
        public LinkedList<UInt32> UInt32s { get; set; }
        public LinkedList<UInt64> UInt64s { get; set; }
        public LinkedList<Int16> Int16s { get; set; }
        public LinkedList<Int32> Int32s { get; set; }
        public LinkedList<Int64> Int64s { get; set; }
        public LinkedList<Double> Doubles { get; set; }
        public LinkedList<Single> Singles { get; set; }
        public LinkedList<Decimal> Decimals { get; set; }
        public LinkedList<string> Strings { get; set; }

        public LinkedLists() { }
        public LinkedLists(int size)
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
        private LinkedList<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            LinkedList<T> list = new LinkedList<T>();
            for (int i = 0; i < size; i++)
            {
                list.AddLast(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
