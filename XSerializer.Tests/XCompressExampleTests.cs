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
    public class XCompressExampleTests
    {
        [TestMethod]
        public void TwoProperties()
        {
            SameReference types = new SameReference();
            string serialize = XObject.XSerialize(types);
            Console.WriteLine($"new SameReference() Serialize:\n{serialize}");
            SameReference deserializeObj = XObject.XDeserialize<SameReference>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew SameReference() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void ListWithText()
        {
            CompressExample compress1 = new CompressExample();
            string serialize = XObject.XSerialize(compress1);
            Console.WriteLine($"new CompressExample() Serialize:\n{serialize}");
            CompressExample deserializeObj = XObject.XDeserialize<CompressExample>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew CompressExample() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void TwoObjects()
        {
            CompressExample2 compress1 = new CompressExample2();
            string serialize = XObject.XSerialize(compress1);
            Console.WriteLine($"new CompressExample2() Serialize:\n{serialize}");
            CompressExample2 deserializeObj = XObject.XDeserialize<CompressExample2>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew CompressExample2() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }



    }
}
