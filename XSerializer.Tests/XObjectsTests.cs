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
            Console.WriteLine($"Serialize:\n{serialize}");
            Types deserializeObj = XObject.XDeserialize<Types>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void Person()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person);
            Console.WriteLine($"Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize.Length == serialize.Length);
        }

    }
}
