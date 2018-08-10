using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class AttributesTests
    {
        [TestMethod]
        public void PropertyIngore()
        {
            WrongPropertyAttribute wrong = new WrongPropertyAttribute();
            string serialize = XObject.XSerialize(wrong, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            WrongPropertyAttribute deserializeObj = XObject.XDeserialize<WrongPropertyAttribute>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void ClassPropertyIngore()
        {
            WrongClassAttribute wrong = new WrongClassAttribute();
            string serialize = XObject.XSerialize(wrong, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            WrongClassAttribute deserializeObj = XObject.XDeserialize<WrongClassAttribute>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }

    }
}
