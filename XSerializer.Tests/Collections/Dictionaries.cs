using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class Dictionaries: ColletionHelper
    {
        public Dictionary<bool, bool> Bools { get; set; }
        public Dictionary<char, char> Chars { get; set; }
        //public Dictionary<sbyte, sbyte> SBytes { get; set; }
        //public Dictionary<UInt16, UInt16> UInt16s { get; set; }
        //public Dictionary<UInt32, UInt32> UInt32s { get; set; }
        //public Dictionary<UInt64, UInt64> UInt64s { get; set; }
        //public Dictionary<Int16, Int16> Int16s { get; set; }
        //public Dictionary<Int32, Int32> Int32s { get; set; }
        //public Dictionary<Int64, Int64> Int64s { get; set; }
        //public Dictionary<Double, Double> Doubles { get; set; }
        //public Dictionary<Single, Single> Singles { get; set; }
        //public Dictionary<Decimal, Decimal> Decimals { get; set; }
        //public Dictionary<string, string> Strings { get; set; }

        public Dictionaries() { }
        public Dictionaries(int size)
        {
            Bools = Adds<bool>(size);
            Chars = Adds<char>(size);
            //SBytes = Adds<sbyte>(size);
            //UInt16s = Adds<UInt16>(size);
            //UInt32s = Adds<UInt32>(size);
            //UInt64s = Adds<UInt64>(size);
            //Int16s = Adds<Int16>(size);
            //Int32s = Adds<Int32>(size);
            //Int64s = Adds<Int64>(size);
            //Doubles = Adds<Double>(size);
            //Singles = Adds<Single>(size);
            //Decimals = Adds<Decimal>(size);
            //Strings = Adds<string>(size);
        }
        private Dictionary<T,T> Adds<T>(int size) {
            size = CutSize<T>(size);
            Dictionary<T,T> list = new Dictionary<T,T>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i), ForSameKey<T>(i));
            }
            return list;
        }
    }
}
