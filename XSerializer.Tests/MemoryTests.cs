using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XSerializer.Tests
{
    [TestClass]
    public class MemoryTests
    {
        [TestMethod]
        public void XMemory()
        {
            Person person = new Person();

            long xMemory = GC.GetTotalMemory(true);
            string resultX = XObject.XSerialize<Person>(person);
            xMemory = GC.GetTotalMemory(true) - xMemory;

            Console.WriteLine($"X length:{resultX.Length},memory:{xMemory}");
        }
    }
}
