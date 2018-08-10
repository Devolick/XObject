using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void ValidClassConsole()
        {
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "`Dzmitry Dym"
            };
            Console.WriteLine(XObject.XSerialize(valid, Mechanism.Weak));
        }
        [TestMethod]
        public void ValidQueryAccentFirst()
        {
            string test = @"&""0'``Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "`Dzmitry Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryAccentEnd()
        {
            string test = @"&""0'Dzmitry Dym``'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym`"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryAccentMiddle()
        {
            string test = @"&""0'Dzmitry `` Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry ` Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryAccentFirst()
        {
            string test = @"&""0'`Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "`Dzmitry Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryAccentEnd()
        {
            string test = @"&""0'Dzmitry Dym`'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym`"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryAccentMiddle()
        {
            string test = @"&""0'Dzmitry ` Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry ` Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }

        [TestMethod]
        public void ValidQueryQuoteFirst()
        {
            string test = @"&""0'''Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "'Dzmitry Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryQuoteEnd()
        {
            string test = @"&""0'Dzmitry Dym'''1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym'"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryQuoteMiddle()
        {
            string test = @"&""0'Dzmitry '' Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry ' Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryQuoteFirst()
        {
            string test = @"&""0''Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "'Dzmitry Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryQuoteEnd()
        {
            string test = @"&""0'Dzmitry Dym''1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry Dym'"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryQuoteMiddle()
        {
            string test = @"&""0'Dzmitry ' Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = "Dzmitry ' Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }

        [TestMethod]
        public void ValidQueryDoubleQuoteFirst()
        {
            string test = @"&""0'""""Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"""Dzmitry Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryDoubleQuoteEnd()
        {
            string test = @"&""0'Dzmitry Dym""""'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"Dzmitry Dym"""
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryDoubleQuoteMiddle()
        {
            string test = @"&""0'Dzmitry """" Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"Dzmitry "" Dym"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryDoubleQuoteFirst()
        {
            string test = @"&""0'""Dzmitry Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"""Dzmitry Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryDoubleQuoteEnd()
        {
            string test = @"&""0'Dzmitry Dym""'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"Dzmitry Dym"""
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryDoubleQuoteMiddle()
        {
            string test = @"&""0'Dzmitry "" Dym'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"Dzmitry "" Dym"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }

        [TestMethod]
        public void ValidQueryOnlyAccents()
        {
            string test = @"&""0'``'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"`"
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryOnlyQuotes()
        {
            string test = @"&""0''''''1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"''"
            };
            Assert.IsTrue(XObject.XSerialize(valid,Mechanism.Weak) == test);
        }
        [TestMethod]
        public void ValidQueryOnlyDoubleQuotes()
        {
            string test = @"&""0'""""""""'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @""""""
            };
            Assert.IsTrue(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryOnlyAccents()
        {
            string test = @"&""0'`'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"`"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryOnlyQuotes()
        {
            string test = @"&""0'''1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @"'"
            };
            Assert.IsFalse(XObject.XSerialize(valid, Mechanism.Weak) == test);
        }
        [TestMethod]
        public void InvalidQueryOnlyDoubleQuotes()
        {
            string test = @"&""0'""""""'1'4'""";
            Valid valid = new Valid()
            {
                Int = 4,
                Str = @""""
            };
            string serialize = XObject.XSerialize(valid, Mechanism.Weak);
            Assert.IsFalse(serialize == test);
        }
    }
}
