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
    public class XObjectsTests
    {
        [TestMethod]
        public void Types()
        {
            Types types = new Types();
            string serialize = XObject.XSerialize(types);
            Console.WriteLine($"new Types() Serialize:\n{serialize}");
            Types deserializeObj = XObject.XDeserialize<Types>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew Types() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Person()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person);
            Console.WriteLine($"new Person() Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew Person() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize.Length == serialize.Length);
        }

    }
}
