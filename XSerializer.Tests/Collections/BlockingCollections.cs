using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class BlockingCollections: ColletionHelper
    {
        public BlockingCollection<bool> Bools { get; set; }
        //public BlockingCollection<char> Chars { get; set; }
        //public BlockingCollection<sbyte> SBytes { get; set; }
        //public BlockingCollection<UInt16> UInt16s { get; set; }
        //public BlockingCollection<UInt32> UInt32s { get; set; }
        //public BlockingCollection<UInt64> UInt64s { get; set; }
        //public BlockingCollection<Int16> Int16s { get; set; }
        //public BlockingCollection<Int32> Int32s { get; set; }
        //public BlockingCollection<Int64> Int64s { get; set; }
        //public BlockingCollection<Double> Doubles { get; set; }
        //public BlockingCollection<Single> Singles { get; set; }
        //public BlockingCollection<Decimal> Decimals { get; set; }
        //public BlockingCollection<string> Strings { get; set; }

        public BlockingCollections() { }
        public BlockingCollections(int size)
        {
            Bools = Adds<bool>(size);
            //Chars = Adds<char>(size);
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
        private BlockingCollection<T> Adds<T>(int size)
        {
            size = CutSize<T>(size);
            BlockingCollection<T> list = new BlockingCollection<T>();
            for (int i = 0; i < size; i++)
            {
                list.Add(ForSameKey<T>(i));
            }
            return list;
        }
    }
}
