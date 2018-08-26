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
    public class CompressExampleTests
    {
        [TestMethod]
        public void TwoProperties()
        {
            SameReference types = new SameReference();
            string serialize = XObject.XSerialize(types, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            SameReference deserializeObj = XObject.XDeserialize<SameReference>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void ListWithText()
        {
            CompressExample compress1 = new CompressExample();
            string serialize = XObject.XSerialize(compress1, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            CompressExample deserializeObj = XObject.XDeserialize<CompressExample>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void TwoObjects()
        {
            CompressExample2 compress1 = new CompressExample2();
            string serialize = XObject.XSerialize(compress1, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            CompressExample2 deserializeObj = XObject.XDeserialize<CompressExample2>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void ItsCompressedWeak()
        {
            string personFile = @"&""1""0'Dzmitry'1'Dym'2'27'3'`2'""2'1234pin'""";
            string serializePerson = XObject.XSerialize(new Person(), Mechanism.Weak);

            Assert.IsTrue(personFile == serializePerson);
        }
        [TestMethod]
        public void ItsCompressedStrong()
        {
            string personFile = @"&""A66C44DE""BC64354D'Dzmitry'C4E51AFD'Dym'6ECB7D8B'27'C9CC1809'`2'""DC32D4EE'1234pin'""";
            string serializePerson = XObject.XSerialize(new Person(), Mechanism.Strong);

            Assert.IsTrue(personFile == serializePerson);
        }


    }
}
