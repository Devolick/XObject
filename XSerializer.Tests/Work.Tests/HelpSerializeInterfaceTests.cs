using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class HelpSerializeInterfaceTests
    {
        [TestMethod]
        public void ClassSerializeChanges()
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
        public void ClassDeserializeChanges()
        {
            string test = @"&""0'old value'";
            Assert.IsTrue(XObject.XDeserialize<ClassHelpInterface>(test, Mechanism.Weak).Value == "XDeserialize");
        }

        [TestMethod]
        public void StructSerializeChanges()
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
        public void StructDeserializeChanges()
        {
            string test = @"&""0'old value'";
            Assert.IsTrue(XObject.XDeserialize<StructHelpInterface>(test, Mechanism.Weak).Value == "XDeserialize");
        }

    }
}
