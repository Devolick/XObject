using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class CloneTests
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
    }
}
