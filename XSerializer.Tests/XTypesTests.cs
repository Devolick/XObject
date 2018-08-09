using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class XTypesTests
    {
        [TestMethod]
        public void Types()
        {
            Types types = new Types();
            string serialize = XObject.XSerialize(types);
            Console.WriteLine($"Serialize:\n{serialize}");
            Types deserializeObj = XObject.XDeserialize<Types>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Bool()
        {
            string serialize = XObject.XSerialize(true);
            Console.WriteLine($"Serialize:\n{serialize}");
            bool deserializeObj = XObject.XDeserialize<bool>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Char()
        {
            string serialize = XObject.XSerialize('A');
            Console.WriteLine($"Serialize:\n{serialize}");
            Char deserializeObj = XObject.XDeserialize<Char>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Sbyte()
        {
            string serialize = XObject.XSerialize(111);
            Console.WriteLine($"Serialize:\n{serialize}");
            sbyte deserializeObj = XObject.XDeserialize<sbyte>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void UInt16()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            UInt16 deserializeObj = XObject.XDeserialize<UInt16>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void UInt32()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            UInt32 deserializeObj = XObject.XDeserialize<UInt32>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void UInt64()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            UInt64 deserializeObj = XObject.XDeserialize<UInt64>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Int16()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            Int16 deserializeObj = XObject.XDeserialize<Int16>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Int32()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            Int32 deserializeObj = XObject.XDeserialize<Int32>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Int64()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            Int64 deserializeObj = XObject.XDeserialize<Int64>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Double()
        {
            string serialize = XObject.XSerialize(1234.123);
            Console.WriteLine($"Serialize:\n{serialize}");
            Double deserializeObj = XObject.XDeserialize<Double>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Single()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            Single deserializeObj = XObject.XDeserialize<Single>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Decimal()
        {
            string serialize = XObject.XSerialize(1234);
            Console.WriteLine($"Serialize:\n{serialize}");
            Decimal deserializeObj = XObject.XDeserialize<Decimal>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void String()
        {
            string serialize = XObject.XSerialize("Hello Dzmitry!");
            Console.WriteLine($"Serialize:\n{serialize}");
            string deserializeObj = XObject.XDeserialize<string>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void NullableInt()
        {
            Nullable<int> n = 777;
            string serialize = XObject.XSerialize(n);
            Console.WriteLine($"Serialize:\n{serialize}");
            Nullable<int> deserializeObj = XObject.XDeserialize<Nullable<int>>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }

    }
}
