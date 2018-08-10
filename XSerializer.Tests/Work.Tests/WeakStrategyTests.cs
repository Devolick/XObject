using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class WeakStrategyTests
    {
        [TestMethod]
        public void SerializeDeserialize()
        {
            Person types = new Person();
            string serialize = XObject.XSerialize(types, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void SerializeDeserializeChanges()
        {
            Person types = new Person();
            string serialize = XObject.XSerialize(types, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize, Mechanism.Weak);
            deserializeObj.Secret = "new Secret 5555";
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsFalse(deserialize == serialize);
        }
        [TestMethod]
        public void EqualStrongAndWeak()
        {
            Person types = new Person();
            string serializeStrong = XObject.XSerialize(types, Mechanism.Strong);
            Console.WriteLine($"Strong Serialize:\n{serializeStrong}");
            string serializeWeak = XObject.XSerialize(types, Mechanism.Weak);
            Console.WriteLine($"Weak Serialize:\n{serializeWeak}");

            Assert.IsFalse(serializeStrong == serializeWeak);
        }
    }
}
