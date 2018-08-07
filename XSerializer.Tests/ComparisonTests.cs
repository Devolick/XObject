using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XSerializer.Tests
{
    [TestClass]
    public class ComparisonTests
    {
        [TestMethod]
        public void JsonMemory()
        {
            Person person = new Person();

            long jsonMemory = GC.GetTotalMemory(true);
            string resultJson = JsonConvert.SerializeObject(person);
            jsonMemory = GC.GetTotalMemory(true) - jsonMemory;

            Console.WriteLine($"Json length:{resultJson.Length},memory:{jsonMemory}");
        }
        [TestMethod]
        public void XMemory()
        {
            Person person = new Person();

            long xMemory = GC.GetTotalMemory(true);
            string resultX = XObject.XSerialize<Person>(person);
            xMemory = GC.GetTotalMemory(true) - xMemory;

            Console.WriteLine($"X length:{resultX.Length},memory:{xMemory}");
        }
        [TestMethod]
        public void XTime()
        {
            Person person = new Person();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            string resultX = XObject.XSerialize<Person>(person);
            watch.Stop();

            Console.WriteLine($"X watch:{watch.Elapsed.Milliseconds}");
        }
        [TestMethod]
        public void JsonTime()
        {
            Person person = new Person();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            string resultJson = JsonConvert.SerializeObject(person);
            watch.Stop();

            Console.WriteLine($"X watch:{watch.Elapsed.Milliseconds}");
        }
    }
}
