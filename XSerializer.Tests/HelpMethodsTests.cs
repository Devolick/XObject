using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class HelpMethodsTests
    {
        [TestMethod]
        public void ClassClone()
        {
            Types types = new Types();
            Types types2 = XObject.Clone(types);

            Assert.IsTrue(XObject.XSerialize(types) == XObject.XSerialize(types2));
        }
        [TestMethod]
        public void StructClone()
        {
            KeyValuePair<string, int> types = new KeyValuePair<string, int>("Dzmitry",1234);
            KeyValuePair<string, int> types2 = XObject.Clone(types);

            Assert.IsTrue(XObject.XSerialize(types) == XObject.XSerialize(types2));
        }

        [TestMethod]
        public void Partially()
        {
            Person person = new Person();
            XObject.Partially(person, "Dennis", "Info.FirstName");
            XObject.Partially(person, "Ritchie", "Info.LastName");

            Assert.IsTrue(
                person.Info.FirstName == "Dennis" && 
                person.Info.LastName == "Ritchie");
        }
        [TestMethod]
        public void Merge()
        {
            Person person1 = new Person();
            Person person2 = new Person();
            person2.Info.FirstName = "Biernat";
            person2.Info.LastName = "Stroustrup";

            XObject.Merge(person1, person2, "Info.FirstName");
            XObject.Merge(person1, person2, "Info.LastName");

            Assert.IsTrue(
                person1.Info.FirstName == "Biernat" &&
                person1.Info.LastName == "Stroustrup");
        }

    }
}
