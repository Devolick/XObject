using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class StrongStrategyTests
    {
        [TestMethod]
        public void SerializeDeserialize()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person, Mechanism.Strong);
            Console.WriteLine($"Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize, Mechanism.Strong);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Strong);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void SerializeDeserializeChanges()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person, Mechanism.Strong);
            Console.WriteLine($"Serialize:\n{serialize}");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize, Mechanism.Strong);
            deserializeObj.Secret = "new Secret 5555";
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Strong);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsFalse(deserialize == serialize);
        }
        [TestMethod]
        public void EqualStrongAndWeak()
        {
            Person person = new Person();
            string serializeStrong = XObject.XSerialize(person, Mechanism.Strong);
            Console.WriteLine($"Strong Serialize:\n{serializeStrong}");
            string serializeWeak = XObject.XSerialize(person, Mechanism.Weak);
            Console.WriteLine($"Weak Serialize:\n{serializeWeak}");

            Assert.IsFalse(serializeStrong == serializeWeak);
        }
        [TestMethod]
        public void StrongDeserializeOldObject()
        {
            Person person = new Person();
            string serializeStrong = XObject.XSerialize(person, Mechanism.Strong);
            Console.WriteLine($"Old Object Serialize:\n{serializeStrong}");
            PersonNew deserializePersonNew = XObject.XDeserialize<PersonNew>(serializeStrong, Mechanism.Strong);
            string serializeStrongNew = XObject.XSerialize(deserializePersonNew, Mechanism.Strong);
            Console.WriteLine($"New Object Serialize:\n{serializeStrongNew}");

            Assert.IsTrue(serializeStrong == serializeStrongNew);
        }
    }
}
