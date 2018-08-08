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
    public class XCollectionsTests
    {
        [TestMethod]
        public void Lists()
        {
            Lists example = new Lists(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            Lists deserializeObj = XObject.XDeserialize<Lists>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");

            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void Dictionaries()
        {
            Dictionaries example = new Dictionaries(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            Dictionaries deserializeObj = XObject.XDeserialize<Dictionaries>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void LinkedLists()
        {
            LinkedLists example = new LinkedLists(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            LinkedLists deserializeObj = XObject.XDeserialize<LinkedLists>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void Queues()
        {
            Queues example = new Queues(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            Queues deserializeObj = XObject.XDeserialize<Queues>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void SortedDictionaries()
        {
            SortedDictionaries example = new SortedDictionaries(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            SortedDictionaries deserializeObj = XObject.XDeserialize<SortedDictionaries>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void SortedLists()
        {
            SortedLists example = new SortedLists(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            SortedLists deserializeObj = XObject.XDeserialize<SortedLists>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"\nDeserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void SortedSets()
        {
            SortedSets example = new SortedSets(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"Serialize:\n{serialize}");
            SortedSets deserializeObj = XObject.XDeserialize<SortedSets>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void Stacks()
        {
            Stacks example = new Stacks(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"new Stacks(32) Serialize:\n{serialize}");
            Stacks deserializeObj = XObject.XDeserialize<Stacks>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"new Stacks(32) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void ConcurrentBags()
        {
            ConcurrentBags example = new ConcurrentBags(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"new ConcurrentBags(32) Serialize:\n{serialize}");
            ConcurrentBags deserializeObj = XObject.XDeserialize<ConcurrentBags>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"new ConcurrentBags(32) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void ConcurrentDictionaries()
        {
            ConcurrentDictionaries example = new ConcurrentDictionaries(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"new ConcurrentDictionaries(32) Serialize:\n{serialize}");
            ConcurrentDictionaries deserializeObj = XObject.XDeserialize<ConcurrentDictionaries>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"new ConcurrentDictionaries(32) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void ConcurrentQueues()
        {
            ConcurrentQueues example = new ConcurrentQueues(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"new ConcurrentQueues(32) Serialize:\n{serialize}");
            ConcurrentQueues deserializeObj = XObject.XDeserialize<ConcurrentQueues>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"new ConcurrentQueues(32) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void ConcurrentStacks()
        {
            ConcurrentStacks example = new ConcurrentStacks(32);
            string serialize = XObject.XSerialize(example);
            Console.WriteLine($"new ConcurrentStacks(32) Serialize:\n{serialize}");
            ConcurrentStacks deserializeObj = XObject.XDeserialize<ConcurrentStacks>(serialize);
            string deserialize = XObject.XSerialize(deserializeObj);
            Console.WriteLine($"new ConcurrentStacks(32) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }

    }
}
