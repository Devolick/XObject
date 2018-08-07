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
            Console.WriteLine($"new Types() Serialize:\n{serialize}");
            Types deserializeObj = XObject.XDeserialize<Types>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nnew Types() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }

    }
}
