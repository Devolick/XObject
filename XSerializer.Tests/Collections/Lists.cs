using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class Lists: ColletionHelper
    {
        public List<bool> Bools { get; set; }
        public List<char> Chars { get; set; }
        //public List<sbyte> SBytes { get; set; }
        //public List<UInt16> UInt16s { get; set; }
        //public List<UInt32> UInt32s { get; set; }
        //public List<UInt64> UInt64s { get; set; }
        //public List<Int16> Int16s { get; set; }
        //public List<Int32> Int32s { get; set; }
        //public List<Int64> Int64s { get; set; }
        //public List<Double> Doubles { get; set; }
        //public List<Single> Singles { get; set; }
        //public List<Decimal> Decimals { get; set; }
        //public List<string> Strings { get; set; }

        public Lists() { }
        public Lists(int size)
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
        private List<T> Adds<T>(int size) {
            size = CutSize<T>(size);
            List<T> list = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
