using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace XSerializer.Tests
{
    [TestClass]
    public class XObjectTests
    {
        [TestMethod]
        public void XObjectSameReferences()
        {
            SameReference types = new SameReference();
            string serialize = XObject.XSerialize<SameReference>(types);
            Console.WriteLine($"new Types() Serialize:\n{serialize}");
            SameReference deserializeObj = XObject.XDeserialize<SameReference>(serialize);
            string deserialize = XObject.XSerialize<SameReference>(deserializeObj);
            Console.WriteLine($"\nnew Types() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void XObjectTypes()
        {
            Types types = new Types();
            string serialize = XObject.XSerialize<Types>(types);
            Console.WriteLine($"new Types() Serialize:\n{serialize}");
            Types deserializeObj = XObject.XDeserialize<Types>(serialize);
            string deserialize = XObject.XSerialize<Types>(deserializeObj);
            Console.WriteLine($"\nnew Types() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize == serialize);
        }
        [TestMethod]
        public void XObjectPerson()
        {
            Person person = new Person();
            string serialize = XObject.XSerialize<Person>(person);
            Console.WriteLine($"new Person() Serialize:\n{serialize}");
            Debug.WriteLine("------------------");
            Person deserializeObj = XObject.XDeserialize<Person>(serialize);
            string deserialize = XObject.XSerialize<Person>(deserializeObj);
            Console.WriteLine($"\nnew Person() Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectLists()
        {
            Lists example = new Lists(1000);
            string serialize = XObject.XSerialize<Lists>(example);
            Console.WriteLine($"new Lists(1000) Serialize:\n{serialize}");
            Lists deserializeObj = XObject.XDeserialize<Lists>(serialize);
            string deserialize = XObject.XSerialize<Lists>(deserializeObj);
            Console.WriteLine($"\nnew Lists(1000) Deserialize:\n{deserialize}");

            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectDictionaries()
        {
            Dictionaries example = new Dictionaries(1000);
            string serialize = XObject.XSerialize<Dictionaries>(example);
            Console.WriteLine($"new Dictionaries(1000) Serialize:\n{serialize}");
            Dictionaries deserializeObj = XObject.XDeserialize<Dictionaries>(serialize);
            string deserialize = XObject.XSerialize<Dictionaries>(deserializeObj);
            Console.WriteLine($"new Dictionaries(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectLinkedLists()
        {
            LinkedLists example = new LinkedLists(1000);
            string serialize = XObject.XSerialize<LinkedLists>(example);
            Console.WriteLine($"new LinkedLists(1000) Serialize:\n{serialize}");
            LinkedLists deserializeObj = XObject.XDeserialize<LinkedLists>(serialize);
            string deserialize = XObject.XSerialize<LinkedLists>(deserializeObj);
            Console.WriteLine($"new LinkedLists(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectQueues()
        {
            Queues example = new Queues(1000);
            string serialize = XObject.XSerialize<Queues>(example);
            Console.WriteLine($"new Queues(1000) Serialize:\n{serialize}");
            Queues deserializeObj = XObject.XDeserialize<Queues>(serialize);
            string deserialize = XObject.XSerialize<Queues>(deserializeObj);
            Console.WriteLine($"new Queues(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectSortedDictionaries()
        {
            SortedDictionaries example = new SortedDictionaries(1000);
            string serialize = XObject.XSerialize<SortedDictionaries>(example);
            Console.WriteLine($"new SortedDictionaries(1000) Serialize:\n{serialize}");
            SortedDictionaries deserializeObj = XObject.XDeserialize<SortedDictionaries>(serialize);
            string deserialize = XObject.XSerialize<SortedDictionaries>(deserializeObj);
            Console.WriteLine($"new SortedDictionaries(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectSortedLists()
        {
            SortedLists example = new SortedLists(1000);
            string serialize = XObject.XSerialize<SortedLists>(example);
            Console.WriteLine($"new SortedLists(1000) Serialize:\n{serialize}");
            SortedLists deserializeObj = XObject.XDeserialize<SortedLists>(serialize);
            string deserialize = XObject.XSerialize<SortedLists>(deserializeObj);
            Console.WriteLine($"new SortedLists(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectSortedSets()
        {
            SortedSets example = new SortedSets(1000);
            string serialize = XObject.XSerialize<SortedSets>(example);
            Console.WriteLine($"new SortedSets(1000) Serialize:\n{serialize}");
            SortedSets deserializeObj = XObject.XDeserialize<SortedSets>(serialize);
            string deserialize = XObject.XSerialize<SortedSets>(deserializeObj);
            Console.WriteLine($"new SortedSets(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectStacks()
        {
            Stacks example = new Stacks(1000);
            string serialize = XObject.XSerialize<Stacks>(example);
            Console.WriteLine($"new Stacks(1000) Serialize:\n{serialize}");
            Stacks deserializeObj = XObject.XDeserialize<Stacks>(serialize);
            string deserialize = XObject.XSerialize<Stacks>(deserializeObj);
            Console.WriteLine($"new Stacks(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectBlockingCollections()
        {
            BlockingCollections example = new BlockingCollections(1000);
            string serialize = XObject.XSerialize<BlockingCollections>(example);
            Console.WriteLine($"new BlockingCollections(1000) Serialize:\n{serialize}");
            BlockingCollections deserializeObj = XObject.XDeserialize<BlockingCollections>(serialize);
            string deserialize = XObject.XSerialize<BlockingCollections>(deserializeObj);
            Console.WriteLine($"new BlockingCollections(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectConcurrentBags()
        {
            ConcurrentBags example = new ConcurrentBags(1000);
            string serialize = XObject.XSerialize<ConcurrentBags>(example);
            Console.WriteLine($"new ConcurrentBags(1000) Serialize:\n{serialize}");
            ConcurrentBags deserializeObj = XObject.XDeserialize<ConcurrentBags>(serialize);
            string deserialize = XObject.XSerialize<ConcurrentBags>(deserializeObj);
            Console.WriteLine($"new ConcurrentBags(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectConcurrentDictionaries()
        {
            ConcurrentDictionaries example = new ConcurrentDictionaries(1000);
            string serialize = XObject.XSerialize<ConcurrentDictionaries>(example);
            Console.WriteLine($"new ConcurrentDictionaries(1000) Serialize:\n{serialize}");
            ConcurrentDictionaries deserializeObj = XObject.XDeserialize<ConcurrentDictionaries>(serialize);
            string deserialize = XObject.XSerialize<ConcurrentDictionaries>(deserializeObj);
            Console.WriteLine($"new ConcurrentDictionaries(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectConcurrentQueues()
        {
            ConcurrentQueues example = new ConcurrentQueues(1000);
            string serialize = XObject.XSerialize<ConcurrentQueues>(example);
            Console.WriteLine($"new ConcurrentQueues(1000) Serialize:\n{serialize}");
            ConcurrentQueues deserializeObj = XObject.XDeserialize<ConcurrentQueues>(serialize);
            string deserialize = XObject.XSerialize<ConcurrentQueues>(deserializeObj);
            Console.WriteLine($"new ConcurrentQueues(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }
        [TestMethod]
        public void XObjectConcurrentStacks()
        {
            ConcurrentStacks example = new ConcurrentStacks(1000);
            string serialize = XObject.XSerialize<ConcurrentStacks>(example);
            Console.WriteLine($"new ConcurrentStacks(1000) Serialize:\n{serialize}");
            ConcurrentStacks deserializeObj = XObject.XDeserialize<ConcurrentStacks>(serialize);
            string deserialize = XObject.XSerialize<ConcurrentStacks>(deserializeObj);
            Console.WriteLine($"new ConcurrentStacks(1000) Deserialize:\n{deserialize}");
            Assert.IsTrue(deserialize.Length == serialize.Length);
        }

    }
}
