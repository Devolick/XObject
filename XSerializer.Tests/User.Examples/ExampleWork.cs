using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class ExampleWork
    {
        [TestMethod]
        public void SerializeWeak()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person, Mechanism.Weak);
            Console.WriteLine(serialize);
        }
        [TestMethod]
        public void DeserializeWeak()
        {
            string person = @"&""1""0'Dzmitry'1'Dym'2'27'3'`2'""2'1234pin'""";
            Person serialize = XObject.XDeserialize<Person>(person, Mechanism.Weak);
            Console.WriteLine(JsonConvert.SerializeObject(serialize));
        }
        [TestMethod]
        public void SerializeStrong()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize(person, Mechanism.Strong);
            Console.WriteLine(serialize);
        }
        [TestMethod]
        public void DeserializeStrong()
        {
            string person = @"&""18C""389'Dzmitry'315'Dym'10D'27'291'`2'""266'1234pin'""";
            Person serialize = XObject.XDeserialize<Person>(person, Mechanism.Strong);
            Console.WriteLine(JsonConvert.SerializeObject(serialize));
        }
        [TestMethod]
        public void IsValid()
        {
            string person = @"&""18C""389'Dzmitry'315'Dym'10D'27'291'`2'""266'1234pin'""";
            Console.WriteLine(XObject.IsValid(person));
        }
        [TestMethod]
        public void Clone()
        {
            Person person = new Person();
            Person personClone = XObject.Clone<Person>(person);
            Console.WriteLine($"person:\n{JsonConvert.SerializeObject(person)}");
            Console.WriteLine($"\npersonClone:\n{JsonConvert.SerializeObject(personClone)}");

            Console.WriteLine($"\n\nIs equal?: {person.Equals(personClone)}");
        }
        [TestMethod]
        public void Merge()
        {
            Person person1 = new Person();
            person1.Info.FirstName = "Dennis";
            person1.Info.LastName = "Ritchie";
            Person person2 = new Person();
            person2.Info.FirstName = "Biernat";
            person2.Info.LastName = "Stroustrup";

            XObject.Merge(person1, person2, "Info.FirstName");
            XObject.Merge(person1, person2, "Info.LastName");

            Console.WriteLine($"person1: {JsonConvert.SerializeObject(person1)}");
            Console.WriteLine($"person2: {JsonConvert.SerializeObject(person2)}");
        }
        [TestMethod]
        public void Partially()
        {
            Person person1 = new Person();
            person1.Info.FirstName = "Biernat";
            person1.Info.LastName = "Stroustrup";

            PersonInfo newInfo = new PersonInfo();
            newInfo.FirstName = "Dennis";
            newInfo.LastName = "Ritchie";

            XObject.Partially(person1, newInfo, "Info");

            Console.WriteLine($"person1: {JsonConvert.SerializeObject(person1)}");
        }
        [TestMethod]
        public void PropertyAttribute()
        {
            //Ignore P2 Property
            WrongPropertyAttribute wrong = new WrongPropertyAttribute();
            string serialize = XObject.XSerialize(wrong, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            WrongPropertyAttribute deserializeObj = XObject.XDeserialize<WrongPropertyAttribute>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void ClassAttribute()
        {
            //Ignore P2 Property
            WrongClassAttribute wrong = new WrongClassAttribute();
            string serialize = XObject.XSerialize(wrong, Mechanism.Weak);
            Console.WriteLine($"Serialize:\n{serialize}");
            WrongClassAttribute deserializeObj = XObject.XDeserialize<WrongClassAttribute>(serialize, Mechanism.Weak);
            string deserialize = XObject.XSerialize(deserializeObj, Mechanism.Weak);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void IXObjectClassSerializeChanges()
        {
            string test = @"&""0'XSerialize'""";
            ClassHelpInterface helpInterface = new ClassHelpInterface()
            {
                Value = "Change in Class"
            };
            Console.WriteLine(XObject.XSerialize(helpInterface, Mechanism.Weak));
            Assert.IsTrue(XObject.XSerialize(helpInterface, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void IXObjectClassDeserializeChanges()
        {
            string test = @"&""0'old value'";
            Assert.IsTrue(XObject.XDeserialize<ClassHelpInterface>(test, Mechanism.Weak).Value == "XDeserialize");
        }

        [TestMethod]
        public void IXObjectStructSerializeChanges()
        {
            string test = @"&""0'XSerialize'""";
            StructHelpInterface helpInterface = new StructHelpInterface()
            {
                Value = "Change in Class"
            };
            Console.WriteLine(XObject.XSerialize(helpInterface, Mechanism.Weak));
            Assert.IsTrue(XObject.XSerialize(helpInterface, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void IXObjectStructDeserializeChanges()
        {
            string test = @"&""0'old value'";
            Assert.IsTrue(XObject.XDeserialize<StructHelpInterface>(test, Mechanism.Weak).Value == "XDeserialize");
        }
    }
}
