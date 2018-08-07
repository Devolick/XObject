using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    [TestClass]
    public class EqualTests
    {
        [TestMethod]
        public void ValidByString()
        {
            string test = @"&""0'Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym"
            };
            Assert.IsTrue(XObject.XSerialize<Valid>(valid) == test);
        }
        [TestMethod]
        public void InvalidByString()
        {
            string test = @"&""0'Dzmitri Dym'1'5'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym"
            };
            Assert.IsFalse(XObject.XSerialize<Valid>(valid) == test);
        }
        [TestMethod]
        public void ValidProtectString()
        {
            string test = @"&""0'``Dzmitri '' """" Dym'1'5'""";
            Valid valid = new Valid()
            {
                Int = 5,
                Str = @"`Dzmitri ' "" Dym"
            };
            Assert.IsTrue(XObject.XSerialize<Valid>(valid) == test);
        }
        [TestMethod]
        public void InvalidProtectString()
        {
            string test = @"&""0'`Dzmitri ' "" Dym'1'5'""";
            Valid valid = new Valid()
            {
                Int = 5,
                Str = @"`Dzmitry ' "" Dym"
            };
            Assert.IsFalse(XObject.XSerialize<Valid>(valid) == test);
        }
    }
}
